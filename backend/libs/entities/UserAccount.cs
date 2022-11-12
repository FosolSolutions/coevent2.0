namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class UserAccount : AuditColumns
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
  public int AccountId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public Account? Account { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected UserAccount() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="user"></param>
  /// <param name="account"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public UserAccount(User user, Account account)
  {
    this.User = user ?? throw new ArgumentNullException(nameof(user));
    this.UserId = user.Id;
    this.Account = account ?? throw new ArgumentNullException(nameof(account));
    this.AccountId = account.Id;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="accountId"></param>
  public UserAccount(long userId, int accountId)
  {
    this.UserId = userId;
    this.AccountId = accountId;
  }
  #endregion
}
