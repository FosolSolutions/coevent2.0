namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class Claim : SortableColumns<int>
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the account this claim belongs to.
  /// </summary>
  public int AccountId { get; set; }

  /// <summary>
  /// get/set - The account this claim belongs to.
  /// </summary>
  public Account? Account { get; set; }

  /// <summary>
  /// get - Collection of roles.
  /// </summary>
  public ICollection<Role> Roles { get; } = new List<Role>();

  /// <summary>
  /// get - Collection of roles (many-to-many).
  /// </summary>
  public ICollection<RoleClaim> RolesManyToMany { get; } = new List<RoleClaim>();
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an Claim object.
  /// </summary>
  protected Claim() { }

  /// <summary>
  /// Creates a new instance of a Claim object, initializes with specified parameters.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="account"></param>
  public Claim(string name, Account account) : base(name)
  {
    this.Account = account ?? throw new ArgumentNullException(nameof(account));
    this.AccountId = account.Id;
  }

  /// <summary>
  /// Creates a new instance of a Claim object, initializes with specified parameters.
  /// </summary>
  /// <param name="name"></param>
  /// <param name="accountId"></param>
  public Claim(string name, int accountId) : base(name)
  {
    this.AccountId = accountId;
  }
  #endregion

  #region Methods
  /// <summary>
  /// Converts the specified 'claim' to a KeyValuePair object.
  /// </summary>
  /// <param name="claim"></param>
  public static implicit operator KeyValuePair<int, string>(Claim claim)
  {
    return new KeyValuePair<int, string>(claim.AccountId, claim.Name);
  }
  #endregion
}
