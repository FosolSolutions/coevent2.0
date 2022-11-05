namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class Claim : SortableColumns<int>
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public Guid Key { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

  /// <summary>
  /// 
  /// </summary>
  public virtual ICollection<RoleClaim> RolesManyToMany { get; set; } = new List<RoleClaim>();
  #endregion
}
