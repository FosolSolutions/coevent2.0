namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class Role : SortableColumns<int>
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public Guid Key { get; set; }

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
  public virtual ICollection<User> Users { get; set; } = new List<User>();

  /// <summary>
  /// 
  /// </summary>
  public virtual ICollection<UserRole> UsersManyToMany { get; set; } = new List<UserRole>();

  /// <summary>
  /// 
  /// </summary>
  public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

  /// <summary>
  /// 
  /// </summary>
  public virtual ICollection<RoleClaim> ClaimsManyToMany { get; set; } = new List<RoleClaim>();
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an Role object.
  /// </summary>
  protected Role() { }

  /// <summary>
  /// Creates new instance of a Role object, initializes with specified parameters.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="key"></param>
  /// <param name="account"></param>
  public Role(string name, Guid key, Account account) : base(name)
  {
    this.Key = key;
    this.Account = account ?? throw new ArgumentNullException(nameof(account));
    this.AccountId = account.Id;
  }
  #endregion
}
