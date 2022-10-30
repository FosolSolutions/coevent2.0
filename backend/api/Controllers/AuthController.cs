using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoEvent.API.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Route("api/[controller]")]
[Route("v{version:apiVersion}/[controller]")]
[Route("[controller]")]
public class AuthController : ControllerBase
{
  #region Constructors
  #endregion

  #region Endpoints
  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  [Authorize]
  [HttpGet("userinfo")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(object), 200)]
  [SwaggerOperation(Tags = new[] { "auth" })]
  public IActionResult UserInfo()
  {
    var user = new { Username = "test" };
    return new JsonResult(new { this.User, user });
  }
  #endregion
}
