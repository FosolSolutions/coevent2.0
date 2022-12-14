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
public class ScheduleEventService : BaseService<ScheduleEvent, long>, IScheduleEventService
{
  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbContext"></param>
  /// <param name="principal"></param>
  /// <param name="serviceProvider"></param>
  /// <param name="logger"></param>
  public ScheduleEventService(CoEventContext dbContext, ClaimsPrincipal principal, IServiceProvider serviceProvider, ILogger<BaseService> logger) : base(dbContext, principal, serviceProvider, logger)
  {
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public override Paging<ScheduleEvent> Find(PageFilter filter)
  {
    var values = (ScheduleEventFilter)filter;
    var query = this.Context.Set<ScheduleEvent>()
      .Include(m => m.Series)
      .Include(m => m.Activities)
        .ThenInclude(m => m.Openings)
          .ThenInclude(o => o.Requirements)
      .Include(m => m.Activities)
        .ThenInclude(m => m.Openings)
          .ThenInclude(m => m.Applications)
            .ThenInclude(m => m.User)
      .Include(m => m.Activities)
        .ThenInclude(m => m.Openings)
          .ThenInclude(m => m.Messages)
            .ThenInclude(m => m.Owner)
      .AsQueryable();

    if (values.ScheduleId.HasValue)
      query = query.Where(i => i.ScheduleId == values.ScheduleId);

    if (!String.IsNullOrWhiteSpace(values.Name))
      query = query.Where(i => EF.Functions.Like(nameof(ScheduleEvent.Name), $"%{values.Name}%"));

    var total = query.Count();

    if (filter.Sort?.Any() == true)
      query = query.OrderByProperty(filter.Sort);
    else
      query = query.OrderBy(i => i.StartOn);

    var items = query
      .AsNoTracking()
      .Skip(filter.Skip)
      .Take(filter.Quantity)
      .ToArray();

    return new Paging<ScheduleEvent>(filter, items, total);
  }
  #endregion
}
