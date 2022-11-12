namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class Account : CommonColumns<int>
{
  #region Properties
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
  public Account(string name, AccountType type, User owner) : base(name)
  {
    this.AccountType = type;
    this.Owner = owner ?? throw new ArgumentNullException(nameof(owner));
    this.OwnerId = owner.Id;
  }
  #endregion
}
