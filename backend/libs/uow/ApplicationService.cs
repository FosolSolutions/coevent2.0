using System.Security.Claims;
using CoEvent.DAL;
using CoEvent.Entities;
using Microsoft.Extensions.Logging;

namespace CoEvent.UoW;

/// <summary>
/// 
/// </summary>
public class ApplicationService : BaseService<Application, long>, IApplicationService
{
  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbContext"></param>
  /// <param name="principal"></param>
  /// <param name="serviceProvider"></param>
  /// <param name="logger"></param>
  public ApplicationService(CoEventContext dbContext, ClaimsPrincipal principal, IServiceProvider serviceProvider, ILogger<BaseService> logger) : base(dbContext, principal, serviceProvider, logger)
  {
  }
  #endregion

  #region Methods
  #endregion
}
