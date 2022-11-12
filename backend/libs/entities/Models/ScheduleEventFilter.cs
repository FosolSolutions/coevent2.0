using CoEvent.Core.Extensions;
using Microsoft.Extensions.Primitives;

namespace CoEvent.Entities.Models;

/// <summary>
/// 
/// </summary>
public class ScheduleEventFilter : PageFilter
{
  #region Properties
  /// <summary>
  /// get/set - Foreign key to the parent schedule.
  /// </summary>
  public long? ScheduleId { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public string? Name { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="url"></param>
  public ScheduleEventFilter(string url) : this(Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(new Uri(url).Query))
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="queryParams"></param>
  public ScheduleEventFilter(Dictionary<string, StringValues> queryParams) : base(queryParams)
  {
    var filter = new Dictionary<string, StringValues>(queryParams, StringComparer.OrdinalIgnoreCase);

    this.ScheduleId = filter.GetLongValue(nameof(this.ScheduleId));
    this.Name = filter.GetStringValue(nameof(this.Name));
  }
  #endregion
}
