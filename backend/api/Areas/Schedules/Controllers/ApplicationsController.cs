using System.Net.Mime;
using CoEvent.Models;
using CoEvent.API.Models;
using CoEvent.UoW;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoEvent.API.Areas.Admin.Controllers;

/// <summary>
/// ApplicationsController class, provides opening application endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Area("schedules")]
[Route("v{version:apiVersion}/[area]/activities/openings/[controller]")]
[Route("[area]/activities/openings/[controller]")]
public class ApplicationsController : ControllerBase
{
  #region Variables
  private readonly IApplicationService _applicationService;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an ApplicationsController object, initializes with specified parameters.
  /// </summary>
  /// <param name="applicationService">DAL service object</param>
  public ApplicationsController(IApplicationService applicationService)
  {
    _applicationService = applicationService;
  }
  #endregion

  #region Endpoints
  /// <summary>
  /// Get the application for the specified 'id'.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id:long}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ApplicationModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "applications-get" })]
  public IActionResult Get(int id)
  {
    var application = _applicationService.FindById(id) ?? throw new KeyNotFoundException("Application not found");
    return new JsonResult(new ApplicationModel(application));
  }

  /// <summary>
  /// Add a new application.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPost]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ApplicationModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "applications-add" })]
  public IActionResult Add(ApplicationModel model)
  {
    var application = _applicationService.AddAndSave((Entities.Application)model);
    application = _applicationService.FindById(application.Id) ?? throw new KeyNotFoundException();
    return CreatedAtAction(nameof(Get), new { id = application.Id }, new ApplicationModel(application));
  }

  /// <summary>
  /// Update the specified application.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPut("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ApplicationModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "applications-update" })]
  public IActionResult Update(ApplicationModel model)
  {
    var application = _applicationService.UpdateAndSave((Entities.Application)model);
    application = _applicationService.FindById(application.Id) ?? throw new KeyNotFoundException();
    return new JsonResult(new ApplicationModel(application));
  }

  /// <summary>
  /// Delete the specified application.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpDelete("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ApplicationModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "applications-delete" })]
  public IActionResult Remove(ApplicationModel model)
  {
    _applicationService.DeleteAndSave((Entities.Application)model);
    return new JsonResult(model);
  }
  #endregion
}
