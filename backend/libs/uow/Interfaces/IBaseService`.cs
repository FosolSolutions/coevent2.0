using CoEvent.Entities.Models;

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
  /// <param name="filter"></param>
  /// <returns></returns>
  Paging<TEntity> Find(PageFilter filter);

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="TModel"></typeparam>
  /// <param name="filter"></param>
  /// <returns></returns>
  Paging<TModel> Find<TModel>(PageFilter filter) where TModel : class;

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
  TEntity AddAndSave(TEntity entity);

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
  /// <returns></returns>
  TEntity UpdateAndSave(TEntity entity);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  void Delete(TEntity entity);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  void DeleteAndSave(TEntity entity);
}
