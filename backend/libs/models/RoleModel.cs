using CoEvent.Core.Extensions;

namespace CoEvent.Models;

/// <summary>
/// 
/// </summary>
public class RoleModel : SortableColumnsModel<int>
{
  #region Properties
  /// <summary>
  /// get/set - 
  /// </summary>
  public Guid Key { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public int AccountId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public AccountModel? Account { get; set; }

  /// <summary>
  /// get/set - Array of claims.
  /// </summary>
  public IEnumerable<ClaimModel> Claims { get; set; } = Array.Empty<ClaimModel>();
  #endregion

  #region Constructors 
  /// <summary>
  /// 
  /// </summary>
  public RoleModel() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="role"></param>
  public RoleModel(Entities.Role role) : base(role)
  {
    this.Key = role.Key;
    this.AccountId = role.AccountId;
    this.Account = role.Account != null ? new AccountModel(role.Account) : null;
    this.Claims = role.Claims.Select(c => new ClaimModel(c)).ToArray();
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="model"></param>
  public static explicit operator Entities.Role(RoleModel model)
  {
    var role = new Entities.Role(model.Name, model.Key, model.AccountId)
    {
      Id = model.Id,
      Description = model.Description,
      IsEnabled = model.IsEnabled,
      SortOrder = model.SortOrder,
      Version = model.Version,
    };

    model.Claims.ForEach(c => role.Claims.Add((Entities.Claim)c));
    return role;
  }
  #endregion
}
