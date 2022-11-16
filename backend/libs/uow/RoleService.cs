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
public class RoleService : BaseService<Role, long>, IRoleService
{
  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbContext"></param>
  /// <param name="principal"></param>
  /// <param name="serviceProvider"></param>
  /// <param name="logger"></param>
  public RoleService(CoEventContext dbContext, ClaimsPrincipal principal, IServiceProvider serviceProvider, ILogger<BaseService> logger) : base(dbContext, principal, serviceProvider, logger)
  {
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public override Paging<Role> Find(PageFilter filter)
  {
    var values = (RoleFilter)filter;
    var query = this.Context.Set<Role>().AsQueryable();

    if (!String.IsNullOrWhiteSpace(values.Name))
      query = query.Where(i => EF.Functions.Like(nameof(Role.Name), $"%{values.Name}%"));

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

    return new Paging<Role>(filter, items, total);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public override Role? FindById(long id)
  {
    return this.Context.Roles
      .Include(u => u.Claims)
      .FirstOrDefault(u => u.Id == id);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  public Role? FindByKey(Guid key)
  {
    return this.Context.Roles
      .Include(u => u.Claims)
      .FirstOrDefault(u => u.Key == key);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public IEnumerable<Role> FindByName(string name)
  {
    return this.Context.Roles
      .AsNoTracking()
      .Include(u => u.Claims)
      .Where(u => EF.Functions.Like(nameof(Role.Name), $"%{name}%"))
      .ToArray();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="accountId"></param>
  /// <returns></returns>
  public IEnumerable<Role> FindByAccount(int accountId)
  {
    return this.Context.Roles
      .AsNoTracking()
      .Include(u => u.Claims)
      .Where(u => u.AccountId == accountId)
      .ToArray();
  }
  #endregion
}
