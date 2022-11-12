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
public class UserService : BaseService<User, long>, IUserService
{
  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbContext"></param>
  /// <param name="principal"></param>
  /// <param name="serviceProvider"></param>
  /// <param name="logger"></param>
  public UserService(CoEventContext dbContext, ClaimsPrincipal principal, IServiceProvider serviceProvider, ILogger<BaseService> logger) : base(dbContext, principal, serviceProvider, logger)
  {
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public override Paging<User> Find(PageFilter filter)
  {
    var values = (UserFilter)filter;
    var query = this.Context.Set<User>().AsQueryable();

    if (!String.IsNullOrWhiteSpace(values.Username))
      query = query.Where(i => EF.Functions.Like(nameof(User.Username), $"%{values.Username}%"));

    var total = query.Count();

    if (filter.Sort?.Any() == true)
      query = query.OrderByProperty(filter.Sort);
    else
      query = query.OrderBy(i => i.Username);

    var items = query
      .AsNoTracking()
      .Skip(filter.Skip)
      .Take(filter.Quantity)
      .ToArray();

    return new Paging<User>(filter, items, total);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public User? FindByKey(Guid key)
  {
    return this.Context.Users
      .Include(u => u.Roles).ThenInclude(r => r.Claims)
      .FirstOrDefault(u => u.Key == key);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="username"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public User? FindByUsername(string username)
  {
    return this.Context.Users
      .Include(u => u.Roles).ThenInclude(r => r.Claims)
      .FirstOrDefault(u => u.Username == username);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="userId"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public IEnumerable<Entities.Claim> GetClaims(long userId)
  {
    return this.Context.UserRoles
      .AsNoTracking()
      .Include(ur => ur.Role).ThenInclude(r => r!.Claims)
      .Where(ur => ur.UserId == userId)
      .SelectMany(ur => ur.Role!.Claims);
  }
  #endregion
}
