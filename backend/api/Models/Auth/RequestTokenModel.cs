using System.Text.Json.Serialization;

namespace CoEvent.API.Models.Auth;

/// <summary>
/// RequestTokenModel class, provides a model that authenticates a username and password.
/// </summary>
public class RequestTokenModel
{
  #region Properties
  /// <summary>
  /// get/set -
  /// </summary>
  [JsonPropertyName("grant_type")]
  public string GrantType { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public string Code { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  [JsonPropertyName("redirect_uri")]
  public string RedirectUri { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  [JsonPropertyName("code_verifier")]
  public string CodeVerifier { get; set; }

  /// <summary>
  /// get/set - The user's username.
  /// </summary>
  public string Username { get; set; }

  /// <summary>
  /// get/set - The user's password.
  /// </summary>
  public string Password { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  [JsonPropertyName("refresh_token")]
  public string RefreshToken { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public string Assertion { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public string Scope { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of a RequestTokenModel object.
  /// </summary>
  public RequestTokenModel()
  {
    this.GrantType = String.Empty;
    this.RedirectUri = String.Empty;
    this.Code = String.Empty;
    this.CodeVerifier = String.Empty;
    this.Username = String.Empty;
    this.Password = String.Empty;
    this.RefreshToken = String.Empty;
    this.Assertion = String.Empty;
    this.Scope = String.Empty;
  }
  #endregion
}
