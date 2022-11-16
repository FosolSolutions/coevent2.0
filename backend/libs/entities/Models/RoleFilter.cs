using CoEvent.Core.Extensions;
using Microsoft.Extensions.Primitives;

namespace CoEvent.Entities.Models;

/// <summary>
/// 
/// </summary>
public class RoleFilter : PageFilter
{
  #region Properties
  /// <summary>
  /// get/set - 
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public int? AccountId { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="url"></param>
  public RoleFilter(string url) : this(Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(new Uri(url).Query))
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="queryParams"></param>
  public RoleFilter(Dictionary<string, StringValues> queryParams) : base(queryParams)
  {
    var filter = new Dictionary<string, StringValues>(queryParams, StringComparer.OrdinalIgnoreCase);

    this.Name = filter.GetStringValue(nameof(this.Name));
    this.AccountId = filter.GetIntNullValue(nameof(this.AccountId));
  }
  #endregion
}
