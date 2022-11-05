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
}
