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
/// UsersController class, provides user admin endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Area("admin")]
[Route("v{version:apiVersion}/[area]/[controller]")]
[Route("[area]/[controller]")]
public class UsersController : ControllerBase
{
  #region Variables
  private readonly IUserService _userService;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an UsersController object, initializes with specified parameters.
  /// </summary>
  /// <param name="userService">DAL service object</param>
  public UsersController(IUserService userService)
  {
    _userService = userService;
  }
  #endregion

  #region Endpoints
  /// <summary>
  /// Get the user for the specified 'id'.
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(UserModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "user-find" })]
  public IActionResult Find()
  {
    var page = _userService.Find<UserModel>(new UserFilter(this.Request.GetDisplayUrl()));
    return new JsonResult(page);
  }

  /// <summary>
  /// Get the user for the specified 'id'.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id:int}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(UserModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "user-get" })]
  public IActionResult Get(int id)
  {
    var user = _userService.FindById(id) ?? throw new KeyNotFoundException("User not found");
    return new JsonResult(new UserModel(user));
  }

  /// <summary>
  /// Add a new user.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPost()]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(UserModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "user-add" })]
  public IActionResult Add(UserModel model)
  {
    var user = _userService.AddAndSave((Entities.User)model);
    return CreatedAtAction(nameof(Get), new { id = user.Id }, new UserModel(user));
  }

  /// <summary>
  /// Update the specified user.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPut("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(UserModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "user-update" })]
  public IActionResult Update(UserModel model)
  {
    var user = _userService.UpdateAndSave((Entities.User)model);
    return new JsonResult(new UserModel(user));
  }

  /// <summary>
  /// Delete the specified user.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpDelete("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(UserModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "user-delete" })]
  public IActionResult Remove(UserModel model)
  {
    _userService.DeleteAndSave((Entities.User)model);
    return new JsonResult(model);
  }
  #endregion
}
