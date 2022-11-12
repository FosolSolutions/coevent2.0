namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class RoleClaim
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public int RoleId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public Role? Role { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public int ClaimId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public Claim? Claim { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an RoleClaim object.
  /// </summary>
  protected RoleClaim() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="role"></param>
  /// <param name="claim"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public RoleClaim(Role role, Claim claim)
  {
    this.Role = role ?? throw new ArgumentNullException(nameof(role));
    this.RoleId = role.Id;
    this.Claim = claim ?? throw new ArgumentNullException(nameof(claim));
    this.ClaimId = claim.Id;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="roleId"></param>
  /// <param name="claimId"></param>
  public RoleClaim(int roleId, int claimId)
  {
    this.RoleId = roleId;
    this.ClaimId = claimId;
  }
  #endregion
}
