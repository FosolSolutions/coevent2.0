namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class Account : CommonColumns<int>
{
  #region Properties
  /// <summary>
  /// get/set - Unique key to identify this account.
  /// </summary>
  public Guid Key { get; set; }

  /// <summary>
  /// get/set - Account type.
  /// </summary>
  public AccountType AccountType { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public long OwnerId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public User? Owner { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public ICollection<User> Users { get; } = new List<User>();

  /// <summary>
  /// 
  /// </summary>
  public ICollection<UserAccount> UsersManyToMany { get; } = new List<UserAccount>();

  /// <summary>
  /// 
  /// </summary>
  public ICollection<Role> Roles { get; } = new List<Role>();

  /// <summary>
  /// 
  /// </summary>
  public ICollection<Claim> Claims { get; } = new List<Claim>();
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an Account object.
  /// </summary>
  protected Account() { }

  /// <summary>
  /// Creates a new instance of an Account object, initializes with specified parameters.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="type"></param>
  /// <param name="owner"></param>
  /// <param name="key"></param>
  public Account(string name, AccountType type, User owner, Guid key) : base(name)
  {
    this.AccountType = type;
    this.Owner = owner ?? throw new ArgumentNullException(nameof(owner));
    this.OwnerId = owner.Id;
    this.Key = key;
  }

  /// <summary>
  /// Creates a new instance of an Account object, initializes with specified parameters.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="type"></param>
  /// <param name="ownerId"></param>
  /// <param name="key"></param>
  public Account(string name, AccountType type, long ownerId, Guid key) : base(name)
  {
    this.AccountType = type;
    this.OwnerId = ownerId;
    this.Key = key;
  }

  /// <summary>
  /// Creates a new instance of an Account object, initializes with specified parameters.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="type"></param>
  /// <param name="owner"></param>
  public Account(string name, AccountType type, User owner) : this(name, type, owner, Guid.NewGuid())
  {
  }

  /// <summary>
  /// Creates a new instance of an Account object, initializes with specified parameters.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="type"></param>
  /// <param name="ownerId"></param>
  public Account(string name, AccountType type, long ownerId) : this(name, type, ownerId, Guid.NewGuid())
  {
  }
  #endregion
}
