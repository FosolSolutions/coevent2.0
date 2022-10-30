namespace CoEvent.DAL.Services.Interfaces;

using CoEvent.DAL.Repositories.Interfaces;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IBaseService<T> : IBaseCrudRepository<T>
    where T : class
{

}
