namespace CoEvent.Models;

/// <summary>
/// 
/// </summary>
public class UserClaimModel : AuditColumnsModel
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public long UserId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public int AccountId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public AccountModel? Account { get; set; }

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
  /// Creates a new instance of an UserClaimModel object.
  /// </summary>
  public UserClaimModel() { }

  /// <summary>
  /// Creates a new instance of an UserClaimModel object, initializes with specified parameters.
  /// </summary>
  /// <param name="claim"></param>
  public UserClaimModel(Entities.UserClaim claim) : base(claim)
  {
    this.UserId = claim.UserId;
    this.AccountId = claim.AccountId;
    this.Account = claim.Account != null ? new AccountModel(claim.Account) : null;
    this.Name = claim.Name;
    this.Value = claim.Value;
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="model"></param>
  public static explicit operator Entities.UserClaim(UserClaimModel model)
  {
    return new Entities.UserClaim(model.UserId, model.AccountId, model.Name, model.Value)
    {
      Version = model.Version,
    };
  }
  #endregion
}
