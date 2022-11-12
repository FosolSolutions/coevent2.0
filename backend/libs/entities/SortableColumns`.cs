namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class SortableColumns<TKey> : CommonColumns<TKey>
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
  /// Creates a new instance of an SortableColumns object.
  /// </summary>
  protected SortableColumns() { }

  /// <summary>
  /// Creates a new instance of a SortableColumns object, initializes with specified parameters.
  /// </summary>
  /// <param name="name"></param>
  public SortableColumns(string name) : base(name)
  {

  }
  #endregion
}
