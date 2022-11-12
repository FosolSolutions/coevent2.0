using CoEvent.Core.Extensions;
using Microsoft.Extensions.Primitives;

namespace CoEvent.Entities.Models;

/// <summary>
/// 
/// </summary>
public class AccountFilter : PageFilter
{
  #region Properties
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
  public AccountFilter(string url) : this(Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(new Uri(url).Query))
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="queryParams"></param>
  public AccountFilter(Dictionary<string, StringValues> queryParams) : base(queryParams)
  {
    var filter = new Dictionary<string, StringValues>(queryParams, StringComparer.OrdinalIgnoreCase);

    this.Name = filter.GetStringValue(nameof(this.Name));
  }
  #endregion
}
