namespace CoEvent.API.Models.Auth;

using CoEvent.Entities;

/// <summary>
/// get/set -
/// </summary>
public class UserModel
{
  #region Properties
  /// <summary>
  /// get/set -
  /// </summary>
  public long Id { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public string Username { get; set; } = default!;

  /// <summary>
  /// get/set -
  /// </summary>
  public string Email { get; set; } = default!;

  /// <summary>
  /// get/set -
  /// </summary>
  public Guid Key { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public string DisplayName { get; set; } = default!;

  /// <summary>
  /// get/set -
  /// </summary>
  public string FirstName { get; set; } = default!;

  /// <summary>
  /// get/set -
  /// </summary>
  public string MiddleName { get; set; } = default!;

  /// <summary>
  /// get/set -
  /// </summary>
  public string LastName { get; set; } = default!;

  /// <summary>
  /// get/set -
  /// </summary>
  public bool IsEnabled { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public int FailedLogins { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public UserType UserType { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public bool EmailVerified { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public DateTime? VerifiedOn { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public ICollection<string> Roles { get; set; } = new List<string>();

  /// <summary>
  /// get/set -
  /// </summary>
  public ICollection<KeyValuePair<string, string>> Claims { get; set; } = new List<KeyValuePair<string, string>>();
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  public UserModel() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="user"></param>
  public UserModel(Entities.User user)
  {
    this.Id = user.Id;
    this.Username = user.Username;
    this.Email = user.Email;
    this.Key = user.Key;
    this.DisplayName = user.DisplayName;
    this.FirstName = user.FirstName;
    this.MiddleName = user.MiddleName;
    this.LastName = user.LastName;
    this.IsEnabled = user.IsEnabled;
    this.FailedLogins = user.FailedLogins;
    this.UserType = user.UserType;
    this.EmailVerified = user.EmailVerified;
    this.VerifiedOn = user.VerifiedOn;
    this.Roles = user.Roles.Select(r => r.Name).ToArray();
    this.Claims = user.Roles.SelectMany(r => r.Claims.Select(c => (KeyValuePair<string, string>)c).ToArray()).ToArray();
  }
  #endregion
}
