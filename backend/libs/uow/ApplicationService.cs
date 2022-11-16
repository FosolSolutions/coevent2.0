using System.Security.Claims;
using CoEvent.DAL;
using CoEvent.Entities;
using Microsoft.EntityFrameworkCore;
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
  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public override Application? FindById(long id)
  {
    return this.Context.Applications
      .Include(m => m.User)
      .FirstOrDefault(m => m.Id == id);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  public override Application Update(Application entity)
  {
    ValidateApplication(entity);
    return base.Update(entity);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  /// <exception cref="KeyNotFoundException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public override Application Add(Application entity)
  {
    ValidateApplication(entity);
    return base.Add(entity);
  }
  #endregion

  #region Helper
  /// <summary>
  /// Validate the application can be made for the user and opening.
  /// Ensure opening exists.
  /// Ensure user exists.
  /// Ensure application limit hasn't been met or exceeded.
  /// Ensure user has claims that match the requirements.
  /// </summary>
  /// <param name="entity"></param>
  /// <exception cref="KeyNotFoundException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  private void ValidateApplication(Application entity)
  {
    // Validate requirements.
    var opening = this.Context.ActivityOpenings
      .Include(o => o.Requirements)
      .Include(o => o.Applications)
      .Include(o => o.Activity).ThenInclude(a => a!.Event).ThenInclude(e => e!.Schedule)
      .FirstOrDefault(o => o.Id == entity.OpeningId) ?? throw new KeyNotFoundException("Opening does not exist");

    var user = this.Context.Users
      .Include(u => u.Claims)
      .FirstOrDefault(u => u.Id == entity.UserId) ?? throw new KeyNotFoundException("User does not exist");

    if (opening.Limit <= opening.Applications.Count) throw new InvalidOperationException("Opening has already been filled");
    if (opening.Requirements.Any() &&
      !opening.Requirements.All(r => user.Claims.Any(c =>
        c.AccountId == opening.Activity!.Event!.Schedule!.AccountId &&
        c.Name == r.Name &&
        c.Value == r.Value))) throw new InvalidOperationException("User does not meet requirements to apply");
  }
  #endregion
}
