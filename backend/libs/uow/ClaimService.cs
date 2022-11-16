using CoEvent.DAL;
using CoEvent.DAL.Extensions;
using CoEvent.Entities;
using CoEvent.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoEvent.UoW;

/// <summary>
/// 
/// </summary>
public class ClaimService : BaseService<Claim, long>, IClaimService
{
  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbContext"></param>
  /// <param name="principal"></param>
  /// <param name="serviceProvider"></param>
  /// <param name="logger"></param>
  public ClaimService(CoEventContext dbContext, System.Security.Claims.ClaimsPrincipal principal, IServiceProvider serviceProvider, ILogger<BaseService> logger) : base(dbContext, principal, serviceProvider, logger)
  {
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public override Paging<Claim> Find(PageFilter filter)
  {
    var values = (ClaimFilter)filter;
    var query = this.Context.Set<Claim>().AsQueryable();

    if (!String.IsNullOrWhiteSpace(values.Name))
      query = query.Where(i => EF.Functions.Like(nameof(Claim.Name), $"%{values.Name}%"));

    if (values.AccountId.HasValue)
      query = query.Where(i => i.AccountId == values.AccountId.Value);

    var total = query.Count();

    if (filter.Sort?.Any() == true)
      query = query.OrderByProperty(filter.Sort);
    else
      query = query.OrderBy(i => i.Name);

    var items = query
      .AsNoTracking()
      .Skip(filter.Skip)
      .Take(filter.Quantity)
      .ToArray();

    return new Paging<Claim>(filter, items, total);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public IEnumerable<Claim> FindByName(string name)
  {
    return this.Context.Claims
      .AsNoTracking()
      .Where(u => EF.Functions.Like(nameof(Claim.Name), $"%{name}%"))
      .ToArray();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="accountId"></param>
  /// <returns></returns>
  public IEnumerable<Claim> FindByAccount(int accountId)
  {
    return this.Context.Claims
      .AsNoTracking()
      .Where(u => u.AccountId == accountId)
      .ToArray();
  }
  #endregion
}
