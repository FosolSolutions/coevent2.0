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
/// RolesController class, provides role admin endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Area("admin")]
[Route("v{version:apiVersion}/[area]/[controller]")]
[Route("[area]/[controller]")]
public class RolesController : ControllerBase
{
  #region Variables
  private readonly IRoleService _roleService;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an RolesController object, initializes with specified parameters.
  /// </summary>
  /// <param name="roleService">DAL service object</param>
  public RolesController(IRoleService roleService)
  {
    _roleService = roleService;
  }
  #endregion

  #region Endpoints
  /// <summary>
  /// Get the role for the specified 'id'.
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(RoleModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "role-find" })]
  public IActionResult Find()
  {
    var page = _roleService.Find<RoleModel>(new RoleFilter(this.Request.GetDisplayUrl()));
    return new JsonResult(page);
  }

  /// <summary>
  /// Get the role for the specified 'id'.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id:long}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(RoleModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "role-get" })]
  public IActionResult Get(long id)
  {
    var role = _roleService.FindById(id) ?? throw new KeyNotFoundException("Role not found");
    return new JsonResult(new RoleModel(role));
  }

  /// <summary>
  /// Add a new role.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPost()]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(RoleModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "role-add" })]
  public IActionResult Add(RoleModel model)
  {
    var role = _roleService.AddAndSave((Entities.Role)model);
    return CreatedAtAction(nameof(Get), new { id = role.Id }, new RoleModel(role));
  }

  /// <summary>
  /// Update the specified role.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPut("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(RoleModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "role-update" })]
  public IActionResult Update(RoleModel model)
  {
    var role = _roleService.UpdateAndSave((Entities.Role)model);
    return new JsonResult(new RoleModel(role));
  }

  /// <summary>
  /// Delete the specified role.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpDelete("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(RoleModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "role-delete" })]
  public IActionResult Remove(RoleModel model)
  {
    _roleService.DeleteAndSave((Entities.Role)model);
    return new JsonResult(model);
  }
  #endregion
}
