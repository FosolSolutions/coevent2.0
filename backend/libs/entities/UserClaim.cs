using System.Diagnostics.CodeAnalysis;

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
  public int AccountId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public Account? Account { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string Name { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public string Value { get; set; } = "";
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected UserClaim() { }

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
  public UserClaim(long userId, int accountId, string name, string value)
  {
    this.UserId = userId;
    this.AccountId = accountId;
    this.Name = name ?? throw new ArgumentNullException(nameof(name));
    this.Value = value ?? throw new ArgumentNullException(nameof(value));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public override bool Equals(object? obj)
  {
    if (obj is not UserClaim entity) return false;
    return (this.UserId, this.AccountId, this.Name, this.Value).Equals((entity.UserId, entity.AccountId, entity.Name, entity.Value));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode()
  {
    return HashCode.Combine(this.UserId, this.AccountId, this.Name, this.Value);
  }
  #endregion
}
