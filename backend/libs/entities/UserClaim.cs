namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class UserClaim : AuditColumns
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public long UserId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public User? User { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public long AccountId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public Account? Account { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string Value { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected UserClaim()
  {
    this.Name = String.Empty;
    this.Value = String.Empty;
    this.User = null!;
    this.Account = null!;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="user"></param>
  /// <param name="account"></param>
  /// <param name="name"></param>
  /// <param name="value"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public UserClaim(User user, Account account, string name, string value)
  {
    this.User = user ?? throw new ArgumentNullException(nameof(user));
    this.UserId = user.Id;
    this.Account = account ?? throw new ArgumentNullException(nameof(account));
    this.AccountId = account.Id;
    this.Name = name ?? throw new ArgumentNullException(nameof(name));
    this.Value = value ?? throw new ArgumentNullException(nameof(value));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="accountId"></param>
  /// <param name="name"></param>
  /// <param name="value"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public UserClaim(long userId, long accountId, string name, string value)
  {
    this.UserId = userId;
    this.AccountId = accountId;
    this.Name = name ?? throw new ArgumentNullException(nameof(name));
    this.Value = value ?? throw new ArgumentNullException(nameof(value));
  }
  #endregion
}
