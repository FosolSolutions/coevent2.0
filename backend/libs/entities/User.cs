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
  public bool IsDisabled { get; set; }

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
  public bool IsVerified { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public DateTime? VerifiedOn { get; set; }
  #endregion
}
