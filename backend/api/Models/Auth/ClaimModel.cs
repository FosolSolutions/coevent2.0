namespace CoEvent.API.Models.Auth;

/// <summary>
/// get/set -
/// </summary>
public class ClaimModel
{
  #region Properties
  /// <summary>
  /// get/set -
  /// </summary>
  public int AccountId { get; set; }

  /// <summary>
  /// get/set -
  /// </summary>
  public string Name { get; set; } = "";

  /// <summary>
  /// get/set -
  /// </summary>
  public string Value { get; set; } = "";
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
  public ClaimModel(Entities.Claim claim)
  {
    this.AccountId = claim.AccountId;
    this.Name = "role";
    this.Value = claim.Name;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="claim"></param>
  public ClaimModel(Entities.UserClaim claim)
  {
    this.AccountId = claim.AccountId;
    this.Name = claim.Name;
    this.Value = claim.Value;
  }
  #endregion
}
