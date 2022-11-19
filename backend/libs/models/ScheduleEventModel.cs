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
  /// get/set - Foreign key to the series.
  /// </summary>
  public int? SeriesId { get; set; }

  /// <summary>
  /// get/set - The series.
  /// </summary>
  public EventSeriesModel? Series { get; set; }

  /// <summary>
  /// get/set - Comma separated list of tags.
  /// </summary>
  public string Tags { get; set; } = "";

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
  public IEnumerable<EventActivityModel> Activities { get; set; } = Array.Empty<EventActivityModel>();
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  public ScheduleEventModel() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public ScheduleEventModel(Entities.ScheduleEvent entity) : base(entity)
  {
    this.ScheduleId = entity.ScheduleId;
    this.SeriesId = entity.SeriesId;
    this.Series = entity.Series != null ? new EventSeriesModel(entity.Series) : null;
    this.StartOn = entity.StartOn;
    this.EndOn = entity.EndOn;
    this.Tags = entity.Tags;
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
      SeriesId = model.SeriesId,
      Description = model.Description,
      IsEnabled = model.IsEnabled,
      SortOrder = model.SortOrder,
      Tags = model.Tags,
      Version = model.Version,
    };
  }
  #endregion
}
