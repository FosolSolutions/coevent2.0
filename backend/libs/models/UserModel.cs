using CoEvent.Core.Extensions;

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
  public string FirstName { get; set; } = "";

  /// <summary>
  /// get/set - 
  /// </summary>
  public string MiddleName { get; set; } = "";

  /// <summary>
  /// get/set - 
  /// </summary>
  public string LastName { get; set; } = "";

  /// <summary>
  /// get/set - 
  /// </summary>
  public string Phone { get; set; } = "";

  /// <summary>
  /// get/set - 
  /// </summary>
  public Entities.UserType UserType { get; set; }

  /// <summary>
  /// get/set - Any array of user claims.
  /// </summary>
  public IEnumerable<UserClaimModel> Claims { get; set; } = Array.Empty<UserClaimModel>();

  /// <summary>
  /// get/set - Any array of roles.
  /// </summary>
  public IEnumerable<RoleModel> Roles { get; set; } = Array.Empty<RoleModel>();

  /// <summary>
  /// get/set - 
  /// </summary>
  public IEnumerable<AccountModel> Accounts { get; set; } = Array.Empty<AccountModel>();
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
    this.FirstName = user.FirstName;
    this.MiddleName = user.MiddleName;
    this.LastName = user.LastName;
    this.Phone = user.Phone;
    this.UserType = user.UserType;
    this.EmailVerified = user.EmailVerified;
    this.Claims = user.Claims.Select(c => new UserClaimModel(c)).ToArray();
    this.Roles = user.Roles.Select(r => new RoleModel(r)).ToArray();
    this.Accounts = user.Accounts.Select(a => new AccountModel(a)).ToArray();
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
    var user = new Entities.User(model.Username, model.Email, model.Key)
    {
      Id = model.Id,
      DisplayName = model.DisplayName,
      IsEnabled = model.IsEnabled,
      FirstName = model.FirstName,
      MiddleName = model.MiddleName,
      LastName = model.LastName,
      Phone = model.Phone,
      UserType = model.UserType,
      EmailVerified = model.EmailVerified,
      Version = model.Version,
    };

    model.Claims.ForEach(c => user.Claims.Add((Entities.UserClaim)c));
    model.Roles.ForEach(r => user.RolesManyToMany.Add(new Entities.UserRole(user, (Entities.Role)r)));
    model.Accounts.ForEach(a => user.AccountsManyToMany.Add(new Entities.UserAccount(user, (Entities.Account)a)));
    return user;
  }
  #endregion
}
