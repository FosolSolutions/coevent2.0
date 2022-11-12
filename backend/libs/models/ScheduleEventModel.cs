namespace CoEvent.Models;

/// <summary>
/// ScheduleEventModel class, provides a model to manage schedule events.
/// An event is a time alloted collection of activities.
/// </summary>
public class ScheduleEventModel : SortableColumnsModel<long>
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the schedule which owns this event.
  /// </summary>
  public long ScheduleId { get; set; }

  /// <summary>
  /// get/set - The schedule.
  /// </summary>
  public ScheduleModel? Schedule { get; set; }

  /// <summary>
  /// get/set - The date and time this schedule begins.
  /// </summary>
  public DateTime StartOn { get; set; }

  /// <summary>
  /// get/set - The date and time this schedule ends.
  /// </summary>
  public DateTime EndOn { get; set; }

  /// <summary>
  /// get - Collection of activities.
  /// </summary>
  public virtual ICollection<EventActivityModel> Activities { get; } = new List<EventActivityModel>();
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected ScheduleEventModel() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public ScheduleEventModel(Entities.ScheduleEvent entity) : base(entity)
  {
    this.ScheduleId = entity.ScheduleId;
    this.StartOn = entity.StartOn;
    this.EndOn = entity.EndOn;
    this.Activities = entity.Activities.Select(a => new EventActivityModel(a)).ToArray();
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="model"></param>
  public static explicit operator Entities.ScheduleEvent(ScheduleEventModel model)
  {
    return new Entities.ScheduleEvent(model.Name, model.ScheduleId, model.StartOn, model.EndOn)
    {
      Id = model.Id,
      Description = model.Description,
      IsEnabled = model.IsEnabled,
      SortOrder = model.SortOrder,
      Version = model.Version,
    };
  }
  #endregion
}
