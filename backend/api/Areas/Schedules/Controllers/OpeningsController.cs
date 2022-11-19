using System.Net.Mime;
using CoEvent.Models;
using CoEvent.API.Models;
using CoEvent.UoW;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CoEvent.Mail;

namespace CoEvent.API.Areas.Admin.Controllers;

/// <summary>
/// OpeningsController class, provides opening opening endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Area("schedules")]
[Route("v{version:apiVersion}/[area]/events/activities/[controller]")]
[Route("[area]/events/activities/[controller]")]
public class OpeningsController : ControllerBase
{
  #region Variables
  private readonly MailClient _client;
  private readonly IOpeningMessageService _openingMessageService;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an OpeningsController object, initializes with specified parameters.
  /// </summary>
  /// <param name="client"></param>
  /// <param name="openingMessageService">DAL service object</param>
  public OpeningsController(MailClient client, IOpeningMessageService openingMessageService)
  {
    _client = client;
    _openingMessageService = openingMessageService;
  }
  #endregion

  #region Endpoints
  /// <summary>
  /// Add a new opening.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPost]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(OpeningMessageModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "openings-add" })]
  public async Task<IActionResult> SendMessageAsync(OpeningMessageModel model)
  {
    var opening = _openingMessageService.AddAndSave((Entities.OpeningMessage)model);
    opening = _openingMessageService.FindById(opening.Id) ?? throw new KeyNotFoundException();

    var mail = _client.CreateOpeningMessage(model);
    await _client.SendAsync(mail);

    return new JsonResult(new OpeningMessageModel(opening));
  }
  #endregion
}
