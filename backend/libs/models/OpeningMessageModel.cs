namespace CoEvent.Models;

/// <summary>
/// OpeningMessageModel class, provides a model to manage 
/// </summary>
public class OpeningMessageModel : AuditColumnsModel
{
  #region Properties
  /// <summary>
  /// get/set - Primary key.
  /// </summary>
  public long Id { get; set; }

  /// <summary>
  /// get/set - Foreign key to the activity opening.
  /// </summary>
  public long OpeningId { get; set; }

  /// <summary>
  /// get/set - The opening.
  /// </summary>
  public ActivityOpeningModel? Opening { get; set; }

  /// <summary>
  /// get/set - Foreign key to user who submitted the application.
  /// </summary>
  public long OwnerId { get; set; }

  /// <summary>
  /// get/set - The user.
  /// </summary>
  public UserModel? Owner { get; set; }

  /// <summary>
  /// get/set - A message to include in the application.
  /// </summary>
  public string Message { get; set; } = "";
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  public OpeningMessageModel() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public OpeningMessageModel(Entities.OpeningMessage entity) : base(entity)
  {
    this.Id = entity.Id;
    this.OwnerId = entity.OwnerId;
    this.Owner = entity.Owner != null ? new UserModel(entity.Owner) : null;
    this.OpeningId = entity.OpeningId;
    this.Message = entity.Message;
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="model"></param>
  public static explicit operator Entities.OpeningMessage(OpeningMessageModel model)
  {
    return new Entities.OpeningMessage(model.OpeningId, model.OwnerId, model.Message)
    {
      Id = model.Id,
      Version = model.Version,
    };
  }
  #endregion
}
