namespace CoEvent.Models;

/// <summary>
/// EventActivityModel class, provides a model to manage event activities.
/// An event activity can have openings which allow users/participants to apply to participate.
/// </summary>
public class EventActivityModel : SortableColumnsModel<long>
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the event that owns this activity.
  /// </summary>
  public long EventId { get; set; }

  /// <summary>
  /// get/set - The event.
  /// </summary>
  public ScheduleEventModel? Event { get; set; }

  /// <summary>
  /// get/set - The date and time this schedule begins.
  /// </summary>
  public DateTime StartOn { get; set; }

  /// <summary>
  /// get/set - The date and time this schedule ends.
  /// </summary>
  public DateTime EndOn { get; set; }

  /// <summary>
  /// get - A collection of openings for this activity.
  /// </summary>
  public IEnumerable<ActivityOpeningModel> Openings { get; set; } = Array.Empty<ActivityOpeningModel>();
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  public EventActivityModel() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public EventActivityModel(Entities.EventActivity entity) : base(entity)
  {
    this.EventId = entity.Id;
    this.StartOn = entity.StartOn;
    this.EndOn = entity.EndOn;
    this.Openings = entity.Openings.Select(o => new ActivityOpeningModel(o)).ToArray();
  }
  #endregion
}
