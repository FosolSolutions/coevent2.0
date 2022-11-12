namespace CoEvent.Entities;

/// <summary>
/// Schedule class, provides a model for managing schedules.
/// A schedule is a time allotted collection of events.
/// </summary>
public class Schedule : SortableColumns<long>
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the account which owns this schedule.
  /// </summary>
  public int AccountId { get; set; }

  /// <summary>
  /// get/set - The account.
  /// </summary>
  public Account? Account { get; set; }

  /// <summary>
  /// get/set - The date and time this schedule begins.
  /// </summary>
  public DateTime StartOn { get; set; }

  /// <summary>
  /// get/set - The date and time this schedule ends.
  /// </summary>
  public DateTime EndOn { get; set; }

  /// <summary>
  /// get - Collection of events.
  /// </summary>
  public virtual ICollection<ScheduleEvent> Events { get; } = new List<ScheduleEvent>();
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of a Schedule object.
  /// </summary>
  protected Schedule() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="account"></param>
  /// <param name="startOn"></param>
  /// <param name="endOn"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public Schedule(string name, Account account, DateTime startOn, DateTime endOn) : base(name)
  {
    this.Account = account ?? throw new ArgumentNullException(nameof(account));
    this.AccountId = account.Id;
    this.StartOn = startOn;
    this.EndOn = endOn;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="accountId"></param>
  /// <param name="startOn"></param>
  /// <param name="endOn"></param>
  public Schedule(string name, int accountId, DateTime startOn, DateTime endOn) : base(name)
  {
    this.AccountId = accountId;
    this.StartOn = startOn;
    this.EndOn = endOn;
  }
  #endregion
}
