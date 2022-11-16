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
/// UserClaimsController class, provides userClaim admin endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Area("admin")]
[Route("v{version:apiVersion}/[area]/user/claims")]
[Route("[area]/user/claims")]
public class UserClaimsController : ControllerBase
{
  #region Variables
  private readonly IUserClaimService _userClaimService;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an UserClaimsController object, initializes with specified parameters.
  /// </summary>
  /// <param name="userClaimService">DAL service object</param>
  public UserClaimsController(IUserClaimService userClaimService)
  {
    _userClaimService = userClaimService;
  }
  #endregion

  #region Endpoints
  /// <summary>
  /// Get the userClaim for the specified 'id'.
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(UserClaimModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "user-claim-find" })]
  public IActionResult Find()
  {
    var page = _userClaimService.Find<UserClaimModel>(new UserClaimFilter(this.Request.GetDisplayUrl()));
    return new JsonResult(page);
  }

  /// <summary>
  /// Get the userClaim for the specified 'id'.
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="accountId"></param>
  /// <param name="name"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  [HttpGet("{userId:long}/{accountId:int}/{name}/{value}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(UserClaimModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "user-claim-get" })]
  public IActionResult Get(long userId, int accountId, string name, string value)
  {
    var userClaim = _userClaimService.FindById((userId, accountId, name, value)) ?? throw new KeyNotFoundException("UserClaim not found");
    return new JsonResult(new UserClaimModel(userClaim));
  }

  /// <summary>
  /// Add a new userClaim.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPost()]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(UserClaimModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "user-claim-add" })]
  public IActionResult Add(UserClaimModel model)
  {
    var userClaim = _userClaimService.AddAndSave((Entities.UserClaim)model);
    return CreatedAtAction(nameof(Get), new { userId = userClaim.UserId, accountId = userClaim.AccountId, name = userClaim.Name, value = userClaim.Value }, new UserClaimModel(userClaim));
  }

  /// <summary>
  /// Update the specified userClaim.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPut("{userId:long}/{accountId:int}/{name}/{value}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(UserClaimModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "user-claim-update" })]
  public IActionResult Update(UserClaimModel model)
  {
    var userClaim = _userClaimService.UpdateAndSave((Entities.UserClaim)model);
    return new JsonResult(new UserClaimModel(userClaim));
  }

  /// <summary>
  /// Delete the specified userClaim.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpDelete("{userId:long}/{accountId:int}/{name}/{value}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(UserClaimModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "user-claim-delete" })]
  public IActionResult Remove(UserClaimModel model)
  {
    _userClaimService.DeleteAndSave((Entities.UserClaim)model);
    return new JsonResult(model);
  }
  #endregion
}
