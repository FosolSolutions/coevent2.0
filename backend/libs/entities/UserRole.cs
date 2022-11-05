namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class UserRole
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public long UserId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public User? User { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public int RoleId { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public Role? Role { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="user"></param>
  /// <param name="role"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public UserRole(User user, Role role)
  {
    this.User = user ?? throw new ArgumentNullException(nameof(user));
    this.UserId = user.Id;
    this.Role = role ?? throw new ArgumentNullException(nameof(role));
    this.RoleId = role.Id;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="roleId"></param>
  public UserRole(long userId, int roleId)
  {
    this.UserId = userId;
    this.RoleId = roleId;
  }
  #endregion
}
