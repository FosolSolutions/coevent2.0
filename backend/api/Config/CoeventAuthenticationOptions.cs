namespace CoEvent.API.Config;

/// <summary>
/// get/set -
/// </summary>
public class CoEventAuthenticationOptions
{
  #region  Properties
  /// <summary>
  /// get/set - The number of characters for the password salt.
  /// </summary>
  public int SaltLength { get; set; }

  /// <summary>
  /// get/set - The issuer of the token.
  /// </summary>
  public string? Issuer { get; set; }

  /// <summary>
  /// get/set - The audience of the token.
  /// </summary>
  public string? Audience { get; set; }

  /// <summary>
  /// get/set - The private key used for signing tokens.
  /// </summary>
  public string? PrivateKey { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public CookieOptions? Cookie { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public TimeSpan AccessTokenExpiresIn { get; set; } = new TimeSpan(0, 1, 0);

  /// <summary>
  /// get/set -
  /// </summary>
  public TimeSpan RefreshTokenExpiresIn { get; set; } = new TimeSpan(1, 0, 0);

  /// <summary>
  /// get/set -
  /// </summary>
  public string DefaultScope { get; set; } = "profile";
  #endregion
}
