using CoEvent.Core.Extensions;
using Microsoft.Extensions.Primitives;

namespace CoEvent.Entities.Models;

/// <summary>
/// 
/// </summary>
public class UserFilter : PageFilter
{
  #region Properties
  /// <summary>
  /// get/set - 
  /// </summary>
  public string? Username { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="url"></param>
  public UserFilter(string url) : this(Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(new Uri(url).Query))
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="queryParams"></param>
  public UserFilter(Dictionary<string, StringValues> queryParams) : base(queryParams)
  {
    var filter = new Dictionary<string, StringValues>(queryParams, StringComparer.OrdinalIgnoreCase);

    this.Username = filter.GetStringValue(nameof(this.Username));
  }
  #endregion
}
