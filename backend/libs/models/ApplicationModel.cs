namespace CoEvent.Models;

/// <summary>
/// ApplicationModel class, provides a model to manage user opening applications.
/// An application is a record of a user applying to an activity opening.
/// </summary>
public class ApplicationModel : AuditColumnsModel
{
  #region Properties
  /// <summary>
  /// get/set - Primary key.
  /// </summary>
  public long Id { get; set; }

  /// <summary>
  /// get/set - Foreign key to user who submitted the application.
  /// </summary>
  public long UserId { get; set; }

  /// <summary>
  /// get/set - The user.
  /// </summary>
  public UserModel? User { get; set; }

  /// <summary>
  /// get/set - Foreign key to the activity opening.
  /// </summary>
  public long OpeningId { get; set; }

  /// <summary>
  /// get/set - The opening.
  /// </summary>
  public ActivityOpeningModel? Opening { get; set; }

  /// <summary>
  /// get/set - A message to include in the application.
  /// </summary>
  public string Message { get; set; } = "";
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected ApplicationModel() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public ApplicationModel(Entities.Application entity) : base(entity)
  {
    this.UserId = entity.Id;
    this.OpeningId = entity.Id;
    this.Message = entity.Message;
  }
  #endregion
}
