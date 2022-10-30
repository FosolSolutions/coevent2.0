namespace CoEvent.DAL.Repositories.Interfaces;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseCrudRepository<T>
    where T : class
{
  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  IEnumerable<T> FindAll();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  IEnumerable<T> FindAllNoTracking();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  T? Find(params object[] key);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  void Add(T entity);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  T AddAndSave(T entity);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  void Update(T entity);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="Entity"></param>
  /// <returns></returns>
  T UpdateAndSave(T Entity);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  void Delete(T entity);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  void DeleteAndSave(T entity);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  T SaveChanges(T entity);
}
