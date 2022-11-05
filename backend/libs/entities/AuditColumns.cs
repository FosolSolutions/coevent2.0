namespace CoEvent.Entities;

using System;
using System.Security.Claims;
using CoEvent.Core.Extensions;

/// <summary>
/// 
/// </summary>
public abstract class AuditColumns
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public DateTime CreatedOn { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string CreatedBy { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public DateTime UpdatedOn { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string UpdatedBy { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public byte[]? Version { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  protected AuditColumns()
  {
    this.CreatedBy = String.Empty;
    this.UpdatedBy = String.Empty;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="createdBy"></param>
  public AuditColumns(string createdBy)
  {
    this.CreatedOn = new DateTime();
    this.CreatedBy = createdBy;
    this.UpdatedOn = new DateTime();
    this.UpdatedBy = createdBy;
  }
  #endregion

  #region Methods
  /// <summary>
  /// Update the audit columns.
  /// </summary>
  /// <param name="user"></param>
  public void OnModified(ClaimsPrincipal? user)
  {
    this.UpdatedBy = user?.GetUsername() ?? "";
    this.UpdatedOn = DateTime.UtcNow;
  }
  #endregion
}
