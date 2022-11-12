namespace CoEvent.UoW;

using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public interface IUserService : IBaseService<User, long>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  User? FindByKey(Guid key);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="username"></param>
  /// <returns></returns>
  User? FindByUsername(string username);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="userId"></param>
  /// <returns></returns>
  IEnumerable<Claim> GetClaims(long userId);
}
