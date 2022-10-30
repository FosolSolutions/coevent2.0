namespace CoEvent.DAL.Repositories;

using CoEvent.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseCrudRepository<T> : IBaseCrudRepository<T>
    where T : class
{
  #region Variables
  private readonly ILogger _logger;
  private readonly CoEventContext _context;
  private readonly IHttpContextAccessor _httpContextAccessor;
  #endregion

  #region Properties
  /// <summary>
  /// 
  /// </summary>
  protected CoEventContext Context { get { return _context; } }

  /// <summary>
  /// 
  /// </summary>
  protected IHttpContextAccessor HttpContextAccessor { get { return _httpContextAccessor; } }
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="httpContextAccessor"></param>
  /// <param name="logger"></param>
  public BaseCrudRepository(CoEventContext context, IHttpContextAccessor httpContextAccessor, ILogger logger)
  {
    _context = context;
    _httpContextAccessor = httpContextAccessor;
    _logger = logger;
  }
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerable<T> FindAll()
  {
    _logger.LogDebug($"Find all '{nameof(T)}'");
    return _context.Set<T>().ToArray();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public IEnumerable<T> FindAllNoTracking()
  {
    _logger.LogDebug($"Find all '{nameof(T)}'");
    return _context.Set<T>().AsNoTracking().ToArray();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  public T? Find(params object[] key)
  {
    _logger.LogDebug($"Find '{nameof(T)}'");
    return _context.Find<T>(key);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public void Add(T entity)
  {
    _context.Add(entity);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  public T AddAndSave(T entity)
  {
    _logger.LogDebug($"Add '{nameof(T)}'");
    this.Add(entity);
    return this.SaveChanges(entity);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public void Update(T entity)
  {
    _context.Update(entity);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  public T UpdateAndSave(T entity)
  {
    _logger.LogDebug($"Update '{nameof(T)}'");
    this.Update(entity);
    return this.SaveChanges(entity);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public void Delete(T entity)
  {
    _context.Remove(entity);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  public void DeleteAndSave(T entity)
  {
    _logger.LogDebug($"Delete '{nameof(T)}'");
    this.Delete(entity);
    this.SaveChanges(entity);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  public T SaveChanges(T entity)
  {
    _context.SaveChanges();
    return entity;
  }
  #endregion
}
