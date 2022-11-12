using System.Security.Claims;
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
public class AccountService : BaseService<Account, int>, IAccountService
{
  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbContext"></param>
  /// <param name="principal"></param>
  /// <param name="serviceProvider"></param>
  /// <param name="logger"></param>
  public AccountService(CoEventContext dbContext, ClaimsPrincipal principal, IServiceProvider serviceProvider, ILogger<BaseService> logger) : base(dbContext, principal, serviceProvider, logger)
  {
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public override Paging<Account> Find(PageFilter filter)
  {
    var values = (AccountFilter)filter;
    var query = this.Context.Set<Account>().AsQueryable();

    if (!String.IsNullOrWhiteSpace(values.Name))
      query = query.Where(i => EF.Functions.Like(nameof(Account.Name), $"%{values.Name}%"));

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

    return new Paging<Account>(filter, items, total);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public Account? FindByKey(Guid key)
  {
    return this.Context.Accounts
      .FirstOrDefault(u => u.Key == key);
  }
  #endregion
}
