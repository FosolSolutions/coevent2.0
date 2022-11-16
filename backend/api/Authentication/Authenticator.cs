namespace CoEvent.API.Authentication;

using System.Security.Claims;
using System.Text;
using CoEvent.API.Config;
using Entities = CoEvent.Entities;
using Microsoft.Extensions.Options;
using CoEvent.API.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CoEvent.DAL.Security;
using CoEvent.Core.Encryption;
using CoEvent.UoW;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

/// <summary>
/// Authenticator class, provides a way to authenticate users.
/// </summary>
public class Authenticator : IAuthenticator
{
  #region Variables
  private readonly CoEventAuthenticationOptions _options;
  private readonly byte[] _privateKey;
  private readonly int _saltLength;
  private readonly IHashPassword _HashPassword;
  private readonly IUserService _dbService;
  private readonly IHttpContextAccessor _httpContext;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an Authenticator object, initializes with specified arguments.
  /// </summary>
  public Authenticator(IOptions<CoEventAuthenticationOptions> options, IHashPassword hashPassword, IUserService dbService, IHttpContextAccessor httpContext)
  {
    if (options.Value.SaltLength <= 0) throw new ArgumentOutOfRangeException(nameof(options), "Authentication:SaltLength configuration is required and must be greater than zero.");

    _options = options.Value;
    _privateKey = Encoding.UTF8.GetBytes(_options.PrivateKey ?? throw new InvalidOperationException("Authentication:PrivateKey configuration is required."));
    _saltLength = _options.SaltLength;
    _HashPassword = hashPassword;
    _dbService = dbService;
    _httpContext = httpContext;
  }
  #endregion

  #region Methods
  /// <summary>
  /// Hash the specified password, with the configured salt.
  /// </summary>
  /// <param name="password"></param>
  /// <returns></returns>
  public string HashPassword(string password)
  {
    return _HashPassword.Hash(password, _saltLength);
  }

  /// <summary>
  /// Find the user for the specified username.
  /// </summary>
  /// <param name="username"></param>
  /// <returns></returns>
  public Entities.User? FindUser(string username)
  {
    return _dbService.FindByUsername(username);
  }

  /// <summary>
  /// Find the user for the specified key.
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  public Entities.User? FindUser(Guid key)
  {
    return _dbService.FindByKey(key);
  }

  /// <summary>
  /// Validate the username and password.
  /// </summary>
  /// <param name="username"></param>
  /// <param name="password"></param>
  /// <returns></returns>
  /// <exception cref="AuthenticationException"></exception>
  public Entities.User Validate(string username, string password)
  {
    var user = FindUser(username);

    if (String.IsNullOrWhiteSpace(user?.Password)) throw new AuthenticationException("Unable to authenticate user.");

    // Extract salt from password.
    var salt = user.Password[.._saltLength];
    var hash = _HashPassword.Hash(password, salt);
    if (user.Password != hash) throw new AuthenticationException("Unable to authenticate user.");

    return user;
  }

  /// <summary>
  /// Authenticate the specified user and create a token for them.
  /// </summary>
  /// <param name="user"></param>
  /// <returns></returns>
  public async Task<TokenModel> AuthenticateAsync(Entities.User user)
  {
    var refreshClaims = new List<Claim>()
    {
        new Claim(ClaimTypes.NameIdentifier, $"{user.Key}", typeof(string).FullName, CoEventIssuer.Issuer, CoEventIssuer.OriginalIssuer),
        new Claim("uid", $"{user.Id}", typeof(long).FullName, CoEventIssuer.Issuer, CoEventIssuer.OriginalIssuer),
        new Claim(CoEventClaimTypes.AccessType, Enum.GetName(user.UserType) ?? String.Empty, typeof(string).FullName, CoEventIssuer.Issuer, CoEventIssuer.OriginalIssuer),
        new Claim(ClaimTypes.Name, $"{user.Username}", typeof(string).FullName, CoEventIssuer.Issuer, CoEventIssuer.OriginalIssuer),
        new Claim(ClaimTypes.Email, $"{user.Email}", typeof(string).FullName, CoEventIssuer.Issuer, CoEventIssuer.OriginalIssuer),
        new Claim(ClaimTypes.GivenName, $"{user.FirstName}", typeof(string).FullName, CoEventIssuer.Issuer, CoEventIssuer.OriginalIssuer),
        new Claim(ClaimTypes.Surname, $"{user.LastName}", typeof(string).FullName, CoEventIssuer.Issuer, CoEventIssuer.OriginalIssuer),
    };
    refreshClaims.AddRange(_dbService.GetRoleClaims(user.Id).Select(c => new Claim(ClaimTypes.Role, c.Name, typeof(string).FullName, CoEventIssuer.Account(c.AccountId), CoEventIssuer.OriginalIssuer)));
    refreshClaims.AddRange(user.Claims.Select(c => new Claim(c.Name, c.Value, typeof(string).FullName, CoEventIssuer.Account(c.AccountId), CoEventIssuer.OriginalIssuer)));
    var identity = new ClaimsIdentity(refreshClaims, JwtBearerDefaults.AuthenticationScheme);
    return await AuthenticationAsync(identity, refreshClaims.ToArray());
  }

  private async Task<TokenModel> AuthenticationAsync(ClaimsIdentity identity, params Claim[] refreshClaims)
  {
    var accessToken = GenerateJwtToken(new ClaimsPrincipal(identity), _options.AccessTokenExpiresIn);
    var refreshToken = _options.RefreshTokenExpiresIn.TotalMilliseconds > 0 ? GenerateJwtToken(GeneratePrincipal(JwtBearerDefaults.AuthenticationScheme, refreshClaims), _options.RefreshTokenExpiresIn) : null;

    // Create a cookie for the user.
    var authProperties = new AuthenticationProperties()
    {
      AllowRefresh = true,
      ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
      IsPersistent = true,
      IssuedUtc = DateTimeOffset.UtcNow
    };
    if (_httpContext.HttpContext != null)
      await _httpContext.HttpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(identity),
        authProperties
      );

    return await Task.FromResult(new TokenModel(accessToken, refreshToken, _options.DefaultScope));
  }

  private static ClaimsPrincipal GeneratePrincipal(string authenticationScheme, params Claim[] claims)
  {
    var identity = new ClaimsIdentity(claims, authenticationScheme);
    return new ClaimsPrincipal(identity);
  }

  private SecurityToken GenerateJwtToken(ClaimsPrincipal user, TimeSpan expiresIn)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Issuer = _options.Issuer,
      Audience = _options.Audience,
      Subject = user.Identity as ClaimsIdentity,
      Expires = DateTime.UtcNow.Add(expiresIn),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_privateKey), SecurityAlgorithms.HmacSha256Signature)
    };
    return tokenHandler.CreateToken(tokenDescriptor);
  }
  #endregion
}
