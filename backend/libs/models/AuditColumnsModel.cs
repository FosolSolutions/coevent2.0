namespace CoEvent.Models;

/// <summary>
/// 
/// </summary>
public abstract class AuditColumnsModel
{

  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public DateTime CreatedOn { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string CreatedBy { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public DateTime UpdatedOn { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public string UpdatedBy { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public byte[]? Version { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  public AuditColumnsModel()
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public AuditColumnsModel(Entities.AuditColumns entity)
  {
    this.CreatedOn = entity.CreatedOn;
    this.CreatedBy = entity.CreatedBy;
    this.UpdatedOn = entity.UpdatedOn;
    this.UpdatedBy = entity.UpdatedBy;
    this.Version = entity.Version;
  }
  #endregion
}
