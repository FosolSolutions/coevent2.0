namespace CoEvent.API.Authentication;

/// <summary>
/// GrantTypes struct, provides grant type options.
/// </summary>
public struct GrantTypes
{
  /// <summary>
  /// Authorization code grant.
  /// </summary>
  public const string AuthorizationCode = "authorization_code";

  /// <summary>
  /// Password grant.
  /// </summary>
  public const string Password = "password";

  /// <summary>
  /// Client credential grant.
  /// </summary>
  public const string ClientCredentials = "client_credentials";

  /// <summary>
  /// Refresh token grant.
  /// </summary>
  public const string RefreshToken = "refresh_token";

  /// <summary>
  /// JWT bearer assertion grant.
  /// </summary>
  public const string JWTBearer = "urn:ietf:params:oauth:grant-type:jwt-bearer";

  /// <summary>
  /// SAML 2.0 bearer assertion grant.
  /// </summary>
  public const string SAML2Bearer = "urn:ietf:params:oauth:grant-type:saml2-bearer";
}
