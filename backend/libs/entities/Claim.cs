namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class Claim : AuditColumns
{
  #region Properties
  /// <summary>
  /// get/set - The primary key.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// get/set - The type of claim.
  /// </summary>
  public string ClaimType { get; set; } = "";

  /// <summary>
  /// get/set - The value of the claim.
  /// </summary>
  public string Value { get; set; } = "";

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
  public virtual ICollection<Role> Roles { get; } = new List<Role>();

  /// <summary>
  /// get - Collection of roles (many-to-many).
  /// </summary>
  public virtual ICollection<RoleClaim> RolesManyToMany { get; } = new List<RoleClaim>();
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an Claim object.
  /// </summary>
  protected Claim() { }

  /// <summary>
  /// Creates a new instance of a Claim object, initializes with specified parameters.
  /// </summary>
  /// <param name="type"></param>
  /// <param name="value"></param>
  /// <param name="account"></param>
  public Claim(string type, string value, Account account)
  {
    this.ClaimType = type;
    this.Value = value;
    this.Account = account ?? throw new ArgumentNullException(nameof(account));
    this.AccountId = account.Id;
  }

  /// <summary>
  /// Creates a new instance of a Claim object, initializes with specified parameters.
  /// </summary>
  /// <param name="type"></param>
  /// <param name="value"></param>
  /// <param name="accountId"></param>
  public Claim(string type, string value, int accountId)
  {
    this.ClaimType = type;
    this.Value = value;
    this.AccountId = accountId;
  }
  #endregion

  #region Methods
  /// <summary>
  /// Converts the specified 'claim' to a KeyValuePair object.
  /// </summary>
  /// <param name="claim"></param>
  public static implicit operator KeyValuePair<string, string>(Claim claim)
  {
    return new KeyValuePair<string, string>(claim.ClaimType, claim.Value);
  }
  #endregion
}
