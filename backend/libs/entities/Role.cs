namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class Role : SortableColumns<int>
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public Guid Key { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public virtual ICollection<User> Users { get; set; } = new List<User>();

  /// <summary>
  /// 
  /// </summary>
  public virtual ICollection<UserRole> UsersManyToMany { get; set; } = new List<UserRole>();

  /// <summary>
  /// 
  /// </summary>
  public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();

  /// <summary>
  /// 
  /// </summary>
  public virtual ICollection<RoleClaim> ClaimsManyToMany { get; set; } = new List<RoleClaim>();
  #endregion
}
