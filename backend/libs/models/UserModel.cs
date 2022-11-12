using CoEvent.Entities;

namespace CoEvent.Models;

/// <summary>
/// 
/// </summary>
public class UserModel : AuditColumnsModel
{
  #region Properties
  /// <summary>
  /// get/set - 
  /// </summary>
  public long Id { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public string Username { get; set; } = "";

  /// <summary>
  /// get/set - 
  /// </summary>
  public Guid Key { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public string Email { get; set; } = "";

  /// <summary>
  /// get/set -
  /// </summary>
  public bool EmailVerified { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public string DisplayName { get; set; } = "";

  /// <summary>
  /// get/set - 
  /// </summary>
  public bool IsEnabled { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public UserType UserType { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an UserModel object.
  /// </summary>
  public UserModel() { }

  /// <summary>
  /// Creates a new instance of an UserModel object, initializes with specified parameters.
  /// </summary>
  /// <param name="user"></param>
  public UserModel(Entities.User user) : base(user)
  {
    this.Id = user.Id;
    this.Username = user.Username;
    this.Key = user.Key;
    this.Email = user.Email;
    this.DisplayName = user.DisplayName;
    this.IsEnabled = user.IsEnabled;
    this.UserType = user.UserType;
    this.EmailVerified = user.EmailVerified;
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public static implicit operator UserModel(Entities.User entity)
  {
    return new UserModel(entity);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="model"></param>
  public static explicit operator Entities.User(UserModel model)
  {
    return new Entities.User(model.Username, model.Email, model.Key)
    {
      Id = model.Id,
      DisplayName = model.DisplayName,
      IsEnabled = model.IsEnabled,
      UserType = model.UserType,
      EmailVerified = model.EmailVerified,
      Version = model.Version,
    };
  }
  #endregion
}
