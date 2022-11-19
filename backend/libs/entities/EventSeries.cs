namespace CoEvent.Entities;

/// <summary>
/// EventSeries class, provides a model to store event account in the database.
/// </summary>
public class EventSeries : SortableColumns<int>
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
  /// get - collection of event series.
  /// </summary>
  public ICollection<ScheduleEvent> Events { get; } = new List<ScheduleEvent>();
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected EventSeries() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="account"></param>
  public EventSeries(string name, Account account) : base(name)
  {
    this.Account = account ?? throw new ArgumentNullException(nameof(account));
    this.AccountId = account.Id;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <param name="accountId"></param>
  public EventSeries(string name, int accountId) : base(name)
  {
    this.AccountId = accountId;
  }
  #endregion
}
