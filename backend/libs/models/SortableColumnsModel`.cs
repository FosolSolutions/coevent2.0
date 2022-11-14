namespace CoEvent.Models;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class SortableColumnsModel<TKey> : CommonColumnsModel<TKey>
  where TKey : notnull
{
  #region Properties
  /// <summary>
  /// 
  /// </summary>
  public int SortOrder { get; set; }
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an SortableColumnsModel object.
  /// </summary>
  public SortableColumnsModel() { }

  /// <summary>
  /// Creates a new instance of a SortableColumnsModel object, initializes with specified parameters.
  /// </summary>
  /// <param name="entity"></param>
  public SortableColumnsModel(Entities.SortableColumns<TKey> entity) : base(entity)
  {
    this.SortOrder = entity.SortOrder;
  }
  #endregion
}
