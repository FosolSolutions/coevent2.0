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
/// MailController class, provides user admin endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Area("admin")]
[Route("v{version:apiVersion}/[area]/[controller]")]
[Route("[area]/[controller]")]
public class MailController : ControllerBase
{
  #region Variables
  private readonly MailClient _client;
  private readonly IUserService _userService;
  private readonly IScheduleService _scheduleService;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an MailController object, initializes with specified parameters.
  /// </summary>
  /// <param name="client">Mail client</param>
  /// <param name="userService"></param>
  /// <param name="scheduleService"></param>
  public MailController(MailClient client, IUserService userService, IScheduleService scheduleService)
  {
    _client = client;
    _userService = userService;
    _scheduleService = scheduleService;
  }
  #endregion

  #region Endpoints
  /// <summary>
  /// Send invitation to specified user.
  /// </summary>
  /// <param name="to"></param>
  /// <returns></returns>
  [HttpPost("invite")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(InvitationModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "mail-invitation" })]
  public async Task<IActionResult> SendInvitationAsync(UserModel to)
  {
    var from = new UserModel(_userService.FindById(1)!);
    var url = new Uri(String.Format("{0}://{1}", this.HttpContext.Request.Scheme, this.HttpContext.Request.Host));
    var schedule = new ScheduleModel(_scheduleService.FindById(1)!);
    var model = new InvitationModel(url, schedule, from, to);
    var mail = _client.CreateInvitation(model);
    await _client.SendAsync(mail);

    return new JsonResult(model);
  }
  #endregion
}
