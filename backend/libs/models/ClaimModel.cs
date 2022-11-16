namespace CoEvent.Models;

/// <summary>
/// 
/// </summary>
public class ClaimModel : SortableColumnsModel<int>
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the account this claim belongs to.
  /// </summary>
  public int AccountId { get; set; }

  /// <summary>
  /// get/set - The account this claim belongs to.
  /// </summary>
  public AccountModel? Account { get; set; }
  #endregion

  #region Constructors 
  /// <summary>
  /// 
  /// </summary>
  public ClaimModel() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="claim"></param>
  public ClaimModel(Entities.Claim claim) : base(claim)
  {
    this.AccountId = claim.AccountId;
    this.Account = claim.Account != null ? new AccountModel(claim.Account) : null;
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="model"></param>
  public static explicit operator Entities.Claim(ClaimModel model)
  {
    return new Entities.Claim(model.Name, model.AccountId)
    {
      Id = model.Id,
      Version = model.Version,
    };
  }
  #endregion
}
