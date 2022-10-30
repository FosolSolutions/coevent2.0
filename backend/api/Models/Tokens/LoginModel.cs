namespace CoEvent.API.Models.Tokens;

/// <summary>
/// LoginModel class, provides a model that authenticates a username and password.
/// </summary>
public class LoginModel
{
  #region Properties
  /// <summary>
  /// get/set - The user's username.
  /// </summary>
  public string Username { get; set; }

  /// <summary>
  /// get/set - The user's password.
  /// </summary>
  public string Password { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of a LoginModel object.
  /// </summary>
  public LoginModel()
  {
    this.Username = String.Empty;
    this.Password = String.Empty;
  }
  #endregion
}
