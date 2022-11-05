namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class User : AuditColumns
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public long Id { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string Username { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public string Email { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public bool EmailVerified { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public Guid Key { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string Password { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public string DisplayName { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public string FirstName { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public string MiddleName { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public string LastName { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public bool IsEnabled { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public int FailedLogins { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public UserType UserType { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public UserStatus Status { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public DateTime? LastLoginOn { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public DateTime? VerifiedOn { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

  /// <summary>
  /// 
  /// </summary>
  public virtual ICollection<UserRole> RolesManyToMany { get; set; } = new List<UserRole>();
  #endregion
}
