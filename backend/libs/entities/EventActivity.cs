namespace CoEvent.Entities;

/// <summary>
/// EventActivity class, provides a model to manage event activities.
/// An event activity can have openings which allow users/participants to apply to participate.
/// </summary>
public class EventActivity : SortableColumns<long>
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the event that owns this activity.
  /// </summary>
  public long EventId { get; set; }

  /// <summary>
  /// get/set - The event.
  /// </summary>
  public ScheduleEvent? Event { get; set; }

  /// <summary>
  /// get/set - The format of this activity.
  /// </summary>
  public string Format { get; set; } = "";

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
  public ICollection<ActivityOpening> Openings { get; } = new List<ActivityOpening>();
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected EventActivity() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="scheduleEvent"></param>
  /// <param name="startOn"></param>
  /// <param name="endOn"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public EventActivity(string name, ScheduleEvent scheduleEvent, DateTime startOn, DateTime endOn) : base(name)
  {
    this.Event = scheduleEvent ?? throw new ArgumentNullException(nameof(scheduleEvent));
    this.EventId = scheduleEvent.Id;
    this.StartOn = startOn;
    this.EndOn = endOn;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="scheduleEventId"></param>
  /// <param name="startOn"></param>
  /// <param name="endOn"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public EventActivity(string name, long scheduleEventId, DateTime startOn, DateTime endOn) : base(name)
  {
    this.EventId = scheduleEventId;
    this.StartOn = startOn;
    this.EndOn = endOn;
  }
  #endregion
}
