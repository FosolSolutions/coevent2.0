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
/// ScheduleEventsController class, provides scheduling endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Area("schedules")]
[Route("v{version:apiVersion}/[area]")]
[Route("[area]")]
public class ScheduleEventsController : ControllerBase
{
  #region Variables
  private readonly IScheduleEventService _scheduleEventService;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an ScheduleEventsController object, initializes with specified parameters.
  /// </summary>
  /// <param name="scheduleEventService">DAL service object</param>
  public ScheduleEventsController(IScheduleEventService scheduleEventService)
  {
    _scheduleEventService = scheduleEventService;
  }
  #endregion

  #region Endpoints
  /// <summary>
  /// Get the scheduleEvent for the specified 'scheduleId'.
  /// </summary>
  /// <param name="scheduleId"></param>
  /// <returns></returns>
  [HttpGet("{scheduleId}/events")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ScheduleEventModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "events-find" })]
  public IActionResult Find(long scheduleId)
  {
    var filter = new ScheduleEventFilter(this.Request.GetDisplayUrl())
    {
      ScheduleId = scheduleId
    };
    var page = _scheduleEventService.Find<ScheduleEventModel>(filter);
    return new JsonResult(page);
  }

  /// <summary>
  /// Get the scheduleEvent for the specified 'id'.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("events/{id:long}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ScheduleEventModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "events-get" })]
  public IActionResult Get(long id)
  {
    var scheduleEvent = _scheduleEventService.FindById(id) ?? throw new KeyNotFoundException("ScheduleEvent not found");
    return new JsonResult(new ScheduleEventModel(scheduleEvent));
  }

  /// <summary>
  /// Add a new scheduleEvent.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPost("events")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ScheduleEventModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "events-add" })]
  public IActionResult Add(ScheduleEventModel model)
  {
    var scheduleEvent = _scheduleEventService.AddAndSave((Entities.ScheduleEvent)model);
    return CreatedAtAction(nameof(Get), new { id = scheduleEvent.Id }, new ScheduleEventModel(scheduleEvent));
  }

  /// <summary>
  /// Update the specified scheduleEvent.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPut("events/{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ScheduleEventModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "events-update" })]
  public IActionResult Update(ScheduleEventModel model)
  {
    var scheduleEvent = _scheduleEventService.UpdateAndSave((Entities.ScheduleEvent)model);
    return new JsonResult(new ScheduleEventModel(scheduleEvent));
  }

  /// <summary>
  /// Delete the specified scheduleEvent.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpDelete("events/{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ScheduleEventModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "events-delete" })]
  public IActionResult Remove(ScheduleEventModel model)
  {
    _scheduleEventService.DeleteAndSave((Entities.ScheduleEvent)model);
    return new JsonResult(model);
  }
  #endregion
}
