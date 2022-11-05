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
}
