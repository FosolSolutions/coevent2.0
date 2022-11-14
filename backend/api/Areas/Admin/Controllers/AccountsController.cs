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
/// AccountsController class, provides account admin endpoints.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Area("admin")]
[Route("v{version:apiVersion}/[area]/[controller]")]
[Route("[area]/[controller]")]
public class AccountsController : ControllerBase
{
  #region Variables
  private readonly IAccountService _accountService;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an AccountsController object, initializes with specified parameters.
  /// </summary>
  /// <param name="accountService">DAL service object</param>
  public AccountsController(IAccountService accountService)
  {
    _accountService = accountService;
  }
  #endregion

  #region Endpoints
  /// <summary>
  /// Get the account for the specified 'id'.
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(AccountModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "account-find" })]
  public IActionResult Find()
  {
    var page = _accountService.Find<AccountModel>(new AccountFilter(this.Request.GetDisplayUrl()));
    return new JsonResult(page);
  }

  /// <summary>
  /// Get the account for the specified 'id'.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  [HttpGet("{id:int}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(AccountModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "account-get" })]
  public IActionResult Get(int id)
  {
    var account = _accountService.FindById(id) ?? throw new KeyNotFoundException("Account not found");
    return new JsonResult(new AccountModel(account));
  }

  /// <summary>
  /// Add a new account.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPost()]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(AccountModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "account-add" })]
  public IActionResult Add(AccountModel model)
  {
    var account = _accountService.AddAndSave((Entities.Account)model);
    return CreatedAtAction(nameof(Get), new { id = account.Id }, new AccountModel(account));
  }

  /// <summary>
  /// Update the specified account.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpPut("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(AccountModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "account-update" })]
  public IActionResult Update(AccountModel model)
  {
    var account = _accountService.UpdateAndSave((Entities.Account)model);
    return new JsonResult(new AccountModel(account));
  }

  /// <summary>
  /// Delete the specified account.
  /// </summary>
  /// <param name="model"></param>
  /// <returns></returns>
  [HttpDelete("{id}")]
  [Produces(MediaTypeNames.Application.Json)]
  [ProducesResponseType(typeof(AccountModel), 200)]
  [ProducesResponseType(typeof(ErrorResponseModel), 400)]
  [SwaggerOperation(Tags = new[] { "account-delete" })]
  public IActionResult Remove(AccountModel model)
  {
    _accountService.DeleteAndSave((Entities.Account)model);
    return new JsonResult(model);
  }
  #endregion
}
