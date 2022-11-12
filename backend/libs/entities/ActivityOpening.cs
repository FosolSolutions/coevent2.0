namespace CoEvent.Entities;

/// <summary>
/// ActivityOpening class, provides a model to manage openings.
/// An opening is an opportunity for users/participants to apply.
/// </summary>
public class ActivityOpening : SortableColumns<long>
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the activity.
  /// </summary>
  public long ActivityId { get; set; }

  /// <summary>
  /// get/set - The activity.
  /// </summary>
  public EventActivity? Activity { get; set; }

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
  public virtual ICollection<Application> Applications { get; } = new List<Application>();
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected ActivityOpening() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="activity"></param>
  /// <param name="limit"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public ActivityOpening(string name, EventActivity activity, int limit) : base(name)
  {
    this.Activity = activity ?? throw new ArgumentNullException(nameof(activity));
    this.ActivityId = activity.Id;
    this.Limit = limit;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="activityId"></param>
  /// <param name="limit"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public ActivityOpening(string name, long activityId, int limit) : base(name)
  {
    this.ActivityId = activityId;
    this.Limit = limit;
  }
  #endregion
}
