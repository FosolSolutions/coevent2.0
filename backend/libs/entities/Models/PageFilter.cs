using CoEvent.Core.Extensions;
using Microsoft.Extensions.Primitives;

namespace CoEvent.Entities.Models;

/// <summary>
/// PageFilter abstract class, provides a model for page filtering.
/// </summary>
public abstract class PageFilter
{
  #region Properties
  /// <summary>
  /// get/set - The page number.
  /// </summary>
  public int Page { get; set; }

  /// <summary>
  /// get/set - Zero based page number;
  /// </summary>
  public int PageIndex { get { return this.Page - 1; } }

  /// <summary>
  /// get/set - The number of items per page.
  /// </summary>
  public int Quantity { get; set; }

  /// <summary>
  /// get - Calculate the number of items to skip based on page and quantity.
  /// </summary>
  public int Skip { get { return this.PageIndex * this.Quantity; } }

  /// <summary>
  /// get/set - Array of columns to sort by.
  /// </summary>
  public string[] Sort { get; set; } = Array.Empty<string>();
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  public PageFilter() { }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="url"></param>
  public PageFilter(string url) : this(Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(new Uri(url).Query))
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="queryParams"></param>
  public PageFilter(Dictionary<string, StringValues> queryParams)
  {
    var filter = new Dictionary<string, StringValues>(queryParams, StringComparer.OrdinalIgnoreCase);

    this.Page = filter.GetIntValue(nameof(this.Page), 1);
    this.Quantity = filter.GetIntValue(nameof(this.Quantity), 10);
    this.Sort = filter.GetStringArrayValue(nameof(this.Sort));
  }
  #endregion
}
