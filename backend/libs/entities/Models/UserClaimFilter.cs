using CoEvent.Core.Extensions;
using Microsoft.Extensions.Primitives;

namespace CoEvent.Entities.Models;

/// <summary>
/// 
/// </summary>
public class UserClaimFilter : PageFilter
{
  #region Properties
  /// <summary>
  /// get/set - 
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// get/set - 
  /// </summary>
  public long? UserId { get; set; }

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
  public UserClaimFilter(string url) : this(Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(new Uri(url).Query))
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="queryParams"></param>
  public UserClaimFilter(Dictionary<string, StringValues> queryParams) : base(queryParams)
  {
    var filter = new Dictionary<string, StringValues>(queryParams, StringComparer.OrdinalIgnoreCase);

    this.Name = filter.GetStringValue(nameof(this.Name));
    this.UserId = filter.GetLongNullValue(nameof(this.UserId));
    this.AccountId = filter.GetIntNullValue(nameof(this.AccountId));
  }
  #endregion
}
