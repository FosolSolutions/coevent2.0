using System.IdentityModel.Tokens.Jwt;
using CoEvent.Core.Extensions;
using Microsoft.IdentityModel.Tokens;

namespace CoEvent.API.Models.Tokens;

/// <summary>
/// TokenModel class, provides a model to represent a successful authentication.
/// </summary>
public class TokenModel
{
  #region Properties
  /// <summary>
  /// get/set - The JWT token.
  /// </summary>
  public string AccessToken { get; set; }

  /// <summary>
  /// get/set - Number of seconds until the access token expires.
  /// </summary>
  public double ExpiresIn { get; set; }

  /// <summary>
  /// get/set - The JWT refresh token.
  /// </summary>
  public string RefreshToken { get; set; }

  /// <summary>
  /// get/set - Number of seconds until the refresh token expires.
  /// </summary>
  public double RefreshExpiresIn { get; set; }

  /// <summary>
  /// get/set - The scope of the token request.
  /// </summary>
  public string Scope { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of a TokenModel object.
  /// </summary>
  public TokenModel()
  {
    this.AccessToken = String.Empty;
    this.RefreshToken = String.Empty;
    this.Scope = String.Empty;
  }

  /// <summary>
  /// Creates a new instance of a TokenModel object, initializes with specified parameters.
  /// </summary>
  /// <param name="accessToken"></param>
  /// <param name="refreshToken"></param>
  /// <param name="scope"></param>
  public TokenModel(SecurityToken accessToken, SecurityToken? refreshToken, string scope)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    this.AccessToken = tokenHandler.WriteToken(accessToken);
    this.ExpiresIn = accessToken.ValidTo.ConvertToUnixTimestamp();
    if (refreshToken != null)
    {
      this.RefreshToken = tokenHandler.WriteToken(refreshToken);
      this.RefreshExpiresIn = refreshToken?.ValidTo.ConvertToUnixTimestamp() ?? 0;
    }
    else this.RefreshToken = "";
    this.Scope = scope;
  }
  #endregion
}
