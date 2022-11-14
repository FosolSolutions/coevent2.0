namespace CoEvent.Models;

/// <summary>
/// ActivityOpeningModel class, provides a model to manage openings.
/// An opening is an opportunity for users/participants to apply.
/// </summary>
public class ActivityOpeningModel : SortableColumnsModel<long>
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the activity.
  /// </summary>
  public long ActivityId { get; set; }

  /// <summary>
  /// get/set - The activity.
  /// </summary>
  public EventActivityModel? Activity { get; set; }

  /// <summary>
  /// get/set - The number of openings of this type that are available.
  /// </summary>
  public int Limit { get; set; }

  /// <summary>
  /// get/set - A question to ask the applicant.
  /// </summary>
  public string Question { get; set; } = "";

  /// <summary>
  /// get/set - Whether the application is required to answer the question.
  /// </summary>
  public bool ResponseRequired { get; set; }

  /// <summary>
  /// get - Collection of applications.
  /// </summary>
  public virtual ICollection<ApplicationModel> Applications { get; } = new List<ApplicationModel>();
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  public ActivityOpeningModel() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public ActivityOpeningModel(Entities.ActivityOpening entity) : base(entity)
  {
    this.ActivityId = entity.ActivityId;
    this.Limit = entity.Limit;
    this.Question = entity.Question;
    this.ResponseRequired = entity.ResponseRequired;
    this.Applications = entity.Applications.Select(a => new ApplicationModel(a)).ToArray();
  }
  #endregion
}
