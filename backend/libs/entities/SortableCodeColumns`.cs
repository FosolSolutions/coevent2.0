namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class SortableCodeColumns<TKey> : SortableColumns<TKey>
  where TKey : notnull
{
  #region Properties
  /// <summary>
  /// get/set - Unique code to identify the item.
  /// </summary>
  public string Code { get; set; } = "";
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of an SortableCodeColumns object.
  /// </summary>
  protected SortableCodeColumns() { }

  /// <summary>
  /// Creates new instance of a SortableCodeColumns object, initializes with specified parameters.
  /// </summary>
  /// <param name="code"></param>
  /// <param name="name"></param>
  public SortableCodeColumns(string code, string name) : base(name)
  {
    this.Code = code;
  }
  #endregion
}
