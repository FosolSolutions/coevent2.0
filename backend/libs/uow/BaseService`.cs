using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CoEvent.Entities;
using CoEvent.DAL;
using CoEvent.Entities.Models;
using CoEvent.DAL.Extensions;

namespace CoEvent.UoW;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public abstract class BaseService<TEntity, TKey> : BaseService, IBaseService<TEntity, TKey>
    where TEntity : class
    where TKey : notnull
{
  #region Properties
  #endregion

  #region Constructors
  /// <summary>
  /// 
  /// </summary>
  /// <param name="dbContext"></param>
  /// <param name="principal"></param>
  /// <param name="serviceProvider"></param>
  /// <param name="logger"></param>
  public BaseService(CoEventContext dbContext, ClaimsPrincipal principal, IServiceProvider serviceProvider, ILogger<BaseService> logger) : base(dbContext, principal, serviceProvider, logger)
  {
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="filter"></param>
  /// <returns></returns>
  public virtual Paging<TEntity> Find(PageFilter filter)
  {
    var query = this.Context.Set<TEntity>().AsQueryable();
    var total = query.Count();

    if (filter.Sort?.Any() == true)
      query = query.OrderByProperty(filter.Sort);

    var items = query
      .AsNoTracking()
      .Skip(filter.Skip)
      .Take(filter.Quantity)
      .ToArray();

    return new Paging<TEntity>(filter, items, total);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="TModel"></typeparam>
  /// <param name="filter"></param>
  /// <returns></returns>
  public virtual Paging<TModel> Find<TModel>(PageFilter filter)
    where TModel : class
  {
    return Find(filter).ToModel<TModel>();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  public virtual TEntity? FindByKey(params object[] key)
  {
    return this.Context.Find<TEntity>(key);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public virtual TEntity? FindById(TKey id)
  {
    return this.Context.Find<TEntity>(id);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public virtual TEntity Add(TEntity entity)
  {
    if (entity == null) throw new ArgumentNullException(nameof(entity));

    this.Context.Entry(entity).State = EntityState.Added;
    this.Context.Add(entity);
    return entity;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public virtual TEntity AddAndSave(TEntity entity)
  {
    Add(entity);
    this.Context.CommitTransaction();
    return entity;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public virtual TEntity Update(TEntity entity)
  {
    if (entity == null) throw new ArgumentNullException(nameof(entity));

    var entry = this.Context.Entry(entity);
    if (entry.State == EntityState.Detached && entity is AuditColumns audit)
    {
      // Fetch the original from the database to ensure the created audit trail is not changed.
      string[] keys = this.Context.Model?.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties.Select(x => x.Name).ToArray() ?? Array.Empty<string>();
      object?[] values = keys.Select(k => typeof(TEntity).GetProperty(k)!.GetValue(entity, null)).Where(v => v != null).ToArray();
      var original = (AuditColumns?)this.Context.Find(typeof(TEntity), values);
      if (original != null)
      {
        this.Context.Entry(original).CurrentValues.SetValues(entity);
        this.Context.Update(original);
        return (original as TEntity)!;
      }
    }

    entry.State = EntityState.Modified;
    this.Context.Update(entity);
    return entity;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public virtual TEntity UpdateAndSave(TEntity entity)
  {
    entity = Update(entity);
    this.Context.CommitTransaction();
    return entity;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public virtual void Delete(TEntity entity)
  {
    if (entity == null) throw new ArgumentNullException(nameof(entity));

    this.Context.Entry(entity).State = EntityState.Deleted;
    this.Context.Remove(entity);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="entity"></param>
  /// <exception cref="ArgumentNullException"></exception>
  public virtual void DeleteAndSave(TEntity entity)
  {
    Delete(entity);
    this.Context.CommitTransaction();
  }
  #endregion
}
