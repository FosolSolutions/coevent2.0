using System.Diagnostics.CodeAnalysis;

namespace CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public class CustomEqualityComparer<T> : IEqualityComparer<T>
{
  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="x"></param>
  /// <param name="y"></param>
  /// <returns></returns>
  public bool Equals(T? x, T? y)
  {
    return x?.Equals(y) ?? false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public int GetHashCode([DisallowNull] T obj)
  {
    return obj.GetHashCode();
  }
  #endregion
}
