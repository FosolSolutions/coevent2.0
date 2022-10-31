namespace CoEvent.UoW;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IBaseService<TEntity, TKey> : IBaseService
    where TEntity : class
    where TKey : notnull
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  TEntity? FindByKey(params object[] key);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  TEntity? FindById(TKey id);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  TEntity Add(TEntity entity);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  TEntity Update(TEntity entity);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  void Delete(TEntity entity);
}
