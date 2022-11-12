using CoEvent.Entities;

namespace CoEvent.Models;

/// <summary>
/// AccountModel class, provides a model for an account.
/// </summary>
public class AccountModel : AuditColumnsModel
{
  #region Properties
  /// <summary>
  /// get/set - 
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public Guid Key { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public string Name { get; set; } = "";

  /// <summary>
  /// get/set - 
  /// </summary>
  public string Description { get; set; } = "";

  /// <summary>
  /// get/set - 
  /// </summary>
  public AccountType AccountType { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public bool IsEnabled { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public long OwnerId { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an AccountModel object.
  /// </summary>
  public AccountModel() { }

  /// <summary>
  /// Creates a new instance of an AccountModel object, initializes with specified parameters.
  /// </summary>
  /// <param name="account"></param>
  public AccountModel(Entities.Account account) : base(account)
  {
    this.Id = account.Id;
    this.Name = account.Name;
    this.Key = account.Key;
    this.Description = account.Description;
    this.AccountType = account.AccountType;
    this.IsEnabled = account.IsEnabled;
    this.OwnerId = account.OwnerId;
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public static implicit operator AccountModel(Entities.Account entity)
  {
    return new AccountModel(entity);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="model"></param>
  public static explicit operator Entities.Account(AccountModel model)
  {
    return new Entities.Account(model.Name, model.AccountType, model.OwnerId, model.Key)
    {
      Id = model.Id,
      Description = model.Description,
      IsEnabled = model.IsEnabled,
      Version = model.Version,
    };
  }
  #endregion
}
