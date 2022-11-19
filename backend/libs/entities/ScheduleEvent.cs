namespace CoEvent.Entities;

/// <summary>
/// ScheduleEvent class, provides a model to manage schedule events.
/// An event is a time alloted collection of activities.
/// </summary>
public class ScheduleEvent : SortableColumns<long>
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the schedule which owns this event.
  /// </summary>
  public long ScheduleId { get; set; }

  /// <summary>
  /// get/set - The schedule.
  /// </summary>
  public Schedule? Schedule { get; set; }

  /// <summary>
  /// get/set - Foreign key to series.
  /// </summary>
  public int? SeriesId { get; set; }

  /// <summary>
  /// get/set - Event series.
  /// </summary>
  public EventSeries? Series { get; set; }

  /// <summary>
  /// get/set - Comma separated list of tags to identify this event.
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
  public ICollection<EventActivity> Activities { get; } = new List<EventActivity>();
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected ScheduleEvent() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="schedule"></param>
  /// <param name="startOn"></param>
  /// <param name="endOn"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public ScheduleEvent(string name, Schedule schedule, DateTime startOn, DateTime endOn) : base(name)
  {
    this.Schedule = schedule ?? throw new ArgumentNullException(nameof(schedule));
    this.ScheduleId = schedule.Id;
    this.StartOn = startOn;
    this.EndOn = endOn;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="scheduleId"></param>
  /// <param name="startOn"></param>
  /// <param name="endOn"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public ScheduleEvent(string name, long scheduleId, DateTime startOn, DateTime endOn) : base(name)
  {
    this.ScheduleId = scheduleId;
    this.StartOn = startOn;
    this.EndOn = endOn;
  }
  #endregion
}
