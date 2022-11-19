using System.Security.Claims;
using CoEvent.DAL;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoEvent.UoW;

/// <summary>
/// 
/// </summary>
public class ActivityOpeningService : BaseService<ActivityOpening, long>, IActivityOpeningService
{
  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbContext"></param>
  /// <param name="principal"></param>
  /// <param name="serviceProvider"></param>
  /// <param name="logger"></param>
  public ActivityOpeningService(CoEventContext dbContext, ClaimsPrincipal principal, IServiceProvider serviceProvider, ILogger<BaseService> logger) : base(dbContext, principal, serviceProvider, logger)
  {
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public override ActivityOpening? FindById(long id)
  {
    return this.Context.ActivityOpenings
      .Include(m => m.Applications)
        .ThenInclude(a => a.User)
      .Include(m => m.Activity)
        .ThenInclude(m => m!.Event)
      .FirstOrDefault(m => m.Id == id);
  }
  #endregion
}
