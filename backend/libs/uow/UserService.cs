using System.Security.Claims;
using CoEvent.Core.Extensions;
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
  /// <param name="id"></param>
  /// <returns></returns>
  public override User? FindById(long id)
  {
    return this.Context.Users
      .Include(u => u.Roles).ThenInclude(r => r.Claims)
      .Include(u => u.Claims)
      .FirstOrDefault(u => u.Id == id);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  public User? FindByKey(Guid key)
  {
    return this.Context.Users
      .Include(u => u.Roles).ThenInclude(r => r.Claims)
      .Include(u => u.Claims)
      .FirstOrDefault(u => u.Key == key);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="username"></param>
  /// <returns></returns>
  public User? FindByUsername(string username)
  {
    return this.Context.Users
      .Include(u => u.Roles).ThenInclude(r => r.Claims)
      .Include(u => u.Claims)
      .FirstOrDefault(u => u.Username == username);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="userId"></param>
  /// <returns></returns>
  public IEnumerable<Entities.Claim> GetRoleClaims(long userId)
  {
    return this.Context.UserRoles
      .AsNoTracking()
      .Include(ur => ur.Role).ThenInclude(r => r!.Claims)
      .Where(ur => ur.UserId == userId)
      .SelectMany(ur => ur.Role!.Claims);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  public override User Add(User entity)
  {
    if (entity.Key == Guid.Empty) entity.Key = Guid.NewGuid();
    this.Context.AddRange(entity.RolesManyToMany.Select(r => { r.User = null; r.Role = null; return r; }));
    this.Context.AddRange(entity.Claims.Select(c => { c.User = null; return c; }));
    return base.Add(entity);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  public override User Update(User entity)
  {
    var user = base.Update(entity);
    user.Password = this.Context.Entry(user).OriginalValues[nameof(User.Password)] as string ?? "";

    var oRoles = this.Context.UserRoles.Where(r => r.UserId == user.Id).ToArray();
    var addRoles = entity.RolesManyToMany.Except(oRoles).Select(r => { r.User = null; r.Role = null; return r; }).ToArray();
    var delRoles = oRoles.Except(entity.RolesManyToMany).Select(r => { r.User = null; r.Role = null; return r; }).ToArray();
    this.Context.RemoveRange(delRoles);
    this.Context.AddRange(addRoles);

    var oClaims = this.Context.UserClaims.Where(c => c.UserId == user.Id).ToArray();
    var addClaims = entity.Claims.Except(oClaims).Select(c => { c.User = null; return c; }).ToArray();
    var delClaims = oClaims.Except(entity.Claims).Select(c => { c.User = null; return c; }).ToArray();
    this.Context.RemoveRange(delClaims);
    this.Context.AddRange(addClaims);

    return user;
  }
  #endregion
}
