using System.Net.Mime;
using CoEvent.Models;
using CoEvent.API.Models;
using CoEvent.UoW;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CoEvent.Mail;
using CoEvent.Models.Mail;

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
  private readonly IUserService _userService;
  private readonly IActivityOpeningService _activityOpeningService;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an OpeningsController object, initializes with specified parameters.
  /// </summary>
  /// <param name="client"></param>
  /// <param name="openingMessageService">DAL service object</param>
  /// <param name="userService">DAL service object</param>
  /// <param name="activityOpeningService">DAL service object</param>
  public OpeningsController(
    MailClient client,
    IOpeningMessageService openingMessageService,
    IUserService userService,
    IActivityOpeningService activityOpeningService)
  {
    _client = client;
    _openingMessageService = openingMessageService;
    _userService = userService;
    _activityOpeningService = activityOpeningService;
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
    var message = _openingMessageService.AddAndSave((Entities.OpeningMessage)model);
    message = _openingMessageService.FindById(message.Id) ?? throw new KeyNotFoundException();

    if (message.Opening?.Applications.Any() ?? false)
    {
      var from = new UserModel(_userService.FindByUsername("jfoster")!);
      var url = new Uri(String.Format("{0}://{1}", this.HttpContext.Request.Scheme, this.HttpContext.Request.Host));
      model.Owner = new UserModel(message.Owner!);
      model.Opening = new ActivityOpeningModel(message.Opening)
      {
        Activity = new EventActivityModel(message.Opening!.Activity!)
      };

      foreach (var applicant in message.Opening!.Applications)
      {
        var messageModel = new OpeningMessageMailModel(url, model, from, applicant.User!);
        var mail = _client.CreateOpeningMessage(messageModel);
        await _client.SendAsync(mail);
      }
    }

    return new JsonResult(new OpeningMessageModel(message));
  }
  #endregion
}
