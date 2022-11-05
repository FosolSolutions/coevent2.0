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
  /// 
  /// </summary>
  public string Code { get; set; } = "";
  #endregion
}
