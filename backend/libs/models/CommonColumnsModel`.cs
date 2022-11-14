namespace CoEvent.Models;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class CommonColumnsModel<TKey> : AuditColumnsModel
  where TKey : notnull
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public TKey Id { get; set; } = default!;

  /// <summary>
  /// 
  /// </summary>
  public string Name { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public string Description { get; set; } = "";

  /// <summary>
  /// 
  /// </summary>
  public bool IsEnabled { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an CommonColumnsModel object.
  /// </summary>
  public CommonColumnsModel() { }

  /// <summary>
  /// Creates a new instance of a CommonColumnsModel object, initializes with specified parameters.
  /// </summary>
  /// <param name="entity"></param>
  public CommonColumnsModel(Entities.CommonColumns<TKey> entity) : base(entity)
  {
    this.Id = entity.Id;
    this.Name = entity.Name;
    this.Description = entity.Description;
    this.IsEnabled = entity.IsEnabled;
  }
  #endregion
}
