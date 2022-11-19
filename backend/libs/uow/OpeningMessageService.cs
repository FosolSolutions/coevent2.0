using System.Security.Claims;
using CoEvent.DAL;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoEvent.UoW;

/// <summary>
/// 
/// </summary>
public class OpeningMessageService : BaseService<OpeningMessage, long>, IOpeningMessageService
{
  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbContext"></param>
  /// <param name="principal"></param>
  /// <param name="serviceProvider"></param>
  /// <param name="logger"></param>
  public OpeningMessageService(CoEventContext dbContext, ClaimsPrincipal principal, IServiceProvider serviceProvider, ILogger<BaseService> logger) : base(dbContext, principal, serviceProvider, logger)
  {
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public override OpeningMessage? FindById(long id)
  {
    return this.Context.OpeningMessages
      .Include(m => m.Owner)
      .Include(m => m.Opening)
        .ThenInclude(m => m!.Applications)
          .ThenInclude(m => m.User)
      .Include(m => m.Opening)
        .ThenInclude(m => m!.Activity)
      .FirstOrDefault(m => m.Id == id);
  }
  #endregion
}
