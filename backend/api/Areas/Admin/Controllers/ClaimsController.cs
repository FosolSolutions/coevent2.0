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
/// ClaimsController class, provides claim admin endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Area("admin")]
[Route("v{version:apiVersion}/[area]/[controller]")]
[Route("[area]/[controller]")]
public class ClaimsController : ControllerBase
{
  #region Variables
  private readonly IClaimService _claimService;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an ClaimsController object, initializes with specified parameters.
  /// </summary>
  /// <param name="claimService">DAL service object</param>
  public ClaimsController(IClaimService claimService)
  {
    _claimService = claimService;
  }
  #endregion

  #region Endpoints
  /// <summary>
  /// Get the claim for the specified 'id'.
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ClaimModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "claim-find" })]
  public IActionResult Find()
  {
    var page = _claimService.Find<ClaimModel>(new ClaimFilter(this.Request.GetDisplayUrl()));
    return new JsonResult(page);
  }

  /// <summary>
  /// Get the claim for the specified 'id'.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id:long}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ClaimModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "claim-get" })]
  public IActionResult Get(long id)
  {
    var claim = _claimService.FindById(id) ?? throw new KeyNotFoundException("Claim not found");
    return new JsonResult(new ClaimModel(claim));
  }

  /// <summary>
  /// Add a new claim.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPost()]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ClaimModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "claim-add" })]
  public IActionResult Add(ClaimModel model)
  {
    var claim = _claimService.AddAndSave((Entities.Claim)model);
    return CreatedAtAction(nameof(Get), new { id = claim.Id }, new ClaimModel(claim));
  }

  /// <summary>
  /// Update the specified claim.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPut("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ClaimModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "claim-update" })]
  public IActionResult Update(ClaimModel model)
  {
    var claim = _claimService.UpdateAndSave((Entities.Claim)model);
    return new JsonResult(new ClaimModel(claim));
  }

  /// <summary>
  /// Delete the specified claim.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpDelete("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(ClaimModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "claim-delete" })]
  public IActionResult Remove(ClaimModel model)
  {
    _claimService.DeleteAndSave((Entities.Claim)model);
    return new JsonResult(model);
  }
  #endregion
}
