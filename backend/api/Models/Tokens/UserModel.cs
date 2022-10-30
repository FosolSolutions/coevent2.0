namespace CoEvent.API.Models.Tokens;

using CoEvent.Entities;

/// <summary>
/// get/set -
/// </summary>
public class UserModel
{
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
  public bool IsDisabled { get; set; }

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
  public bool IsVerified { get; set; }

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
}
