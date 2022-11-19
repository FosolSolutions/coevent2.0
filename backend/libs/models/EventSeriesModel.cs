namespace CoEvent.Models;

/// <summary>
/// EventSeriesModel class, provides a model to manage event series.
/// A series is a group of related events.
/// </summary>
public class EventSeriesModel : SortableColumnsModel<int>
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the account.
  /// </summary>
  public int AccountId { get; set; }

  /// <summary>
  /// get/set - The account.
  /// </summary>
  public AccountModel? Account { get; set; }

  /// <summary>
  /// get - Collection of activities.
  /// </summary>
  public IEnumerable<ScheduleEventModel> Events { get; set; } = Array.Empty<ScheduleEventModel>();
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  public EventSeriesModel() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public EventSeriesModel(Entities.EventSeries entity) : base(entity)
  {
    this.AccountId = entity.AccountId;
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="model"></param>
  public static explicit operator Entities.EventSeries(EventSeriesModel model)
  {
    return new Entities.EventSeries(model.Name, model.AccountId)
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
