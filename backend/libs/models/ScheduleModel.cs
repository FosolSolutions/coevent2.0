namespace CoEvent.Models;

/// <summary>
/// Schedule class, provides a model for managing schedules.
/// A schedule is a time allotted collection of events.
/// </summary>
public class ScheduleModel : SortableColumnsModel<long>
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the account which owns this schedule.
  /// </summary>
  public int AccountId { get; set; }

  /// <summary>
  /// get/set - The account.
  /// </summary>
  public AccountModel? Account { get; set; }

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
  public virtual ICollection<ScheduleEventModel> Events { get; } = new List<ScheduleEventModel>();
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of a ScheduleModel object.
  /// </summary>
  public ScheduleModel() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public ScheduleModel(Entities.Schedule entity) : base(entity)
  {
    this.AccountId = entity.AccountId;
    this.Account = entity.Account != null ? new AccountModel(entity.Account) : null;
    this.StartOn = entity.StartOn;
    this.EndOn = entity.EndOn;
    this.Events = entity.Events.Select(e => new ScheduleEventModel(e)).ToArray();
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="model"></param>
  public static explicit operator Entities.Schedule(ScheduleModel model)
  {
    return new Entities.Schedule(model.Name, model.AccountId, model.StartOn, model.EndOn)
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
