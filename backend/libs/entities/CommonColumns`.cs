namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class CommonColumns<TKey> : AuditColumns
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
  /// Creates a new instance of an CommonColumns object.
  /// </summary>
  protected CommonColumns() { }

  /// <summary>
  /// Creates a new instance of a CommonColumns object, initializes with specified parameters.
  /// </summary>
  /// <param name="name"></param>
  public CommonColumns(string name)
  {
    this.Name = name;
  }
  #endregion
}
