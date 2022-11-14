using System.Net.Mime;
using CoEvent.Models;
using CoEvent.API.Models;
using CoEvent.UoW;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Http.Extensions;
using CoEvent.Entities.Models;

namespace CoEvent.API.Areas.Admin.Controllers;

/// <summary>
/// SchedulesController class, provides scheduling endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Area("schedules")]
[Route("v{version:apiVersion}/[controller]")]
[Route("[controller]")]
public class SchedulesController : ControllerBase
{
  #region Variables
  private readonly IScheduleService _scheduleService;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an SchedulesController object, initializes with specified parameters.
  /// </summary>
  /// <param name="scheduleService">DAL service object</param>
  public SchedulesController(IScheduleService scheduleService)
  {
    _scheduleService = scheduleService;
  }
  #endregion

  #region Endpoints
  /// <summary>
  /// Get the schedule for the specified 'id'.
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ScheduleModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "schedule-find" })]
  public IActionResult Find()
  {
    var page = _scheduleService.Find<ScheduleModel>(new ScheduleFilter(this.Request.GetDisplayUrl()));
    return new JsonResult(page);
  }

  /// <summary>
  /// Get the schedule for the specified 'id'.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id:long}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ScheduleModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "schedule-get" })]
  public IActionResult Get(long id)
  {
    var schedule = _scheduleService.FindById(id) ?? throw new KeyNotFoundException("Schedule not found");
    return new JsonResult(new ScheduleModel(schedule));
  }

  /// <summary>
  /// Add a new schedule.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPost()]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ScheduleModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "schedule-add" })]
  public IActionResult Add(ScheduleModel model)
  {
    var schedule = _scheduleService.AddAndSave((Entities.Schedule)model);
    return CreatedAtAction(nameof(Get), new { id = schedule.Id }, new ScheduleModel(schedule));
  }

  /// <summary>
  /// Update the specified schedule.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPut("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ScheduleModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "schedule-update" })]
  public IActionResult Update(ScheduleModel model)
  {
    var schedule = _scheduleService.UpdateAndSave((Entities.Schedule)model);
    return new JsonResult(new ScheduleModel(schedule));
  }

  /// <summary>
  /// Delete the specified schedule.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpDelete("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ScheduleModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "schedule-delete" })]
  public IActionResult Remove(ScheduleModel model)
  {
    _scheduleService.DeleteAndSave((Entities.Schedule)model);
    return new JsonResult(model);
  }
  #endregion
}
