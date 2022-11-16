namespace CoEvent.UoW;

using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public interface IUserClaimService : IBaseService<UserClaim, (long UserId, int AccountId, string Name, string Value)>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="userId"></param>
  /// <returns></returns>
  IEnumerable<UserClaim> FindByUser(long userId);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="accountId"></param>
  /// <returns></returns>
  IEnumerable<UserClaim> FindByAccount(int accountId);
}
