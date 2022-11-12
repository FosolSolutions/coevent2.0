namespace CoEvent.UoW;

using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public interface IAccountService : IBaseService<Account, int>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="key"></param>
  /// <returns></returns>
  Account? FindByKey(Guid key);
}
