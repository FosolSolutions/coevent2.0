namespace CoEvent.UoW;

using CoEvent.Entities;

/// <summary>
/// 
/// </summary>
public interface IClaimService : IBaseService<Claim, long>
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  IEnumerable<Claim> FindByName(string name);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="accountId"></param>
  /// <returns></returns>
  IEnumerable<Claim> FindByAccount(int accountId);
}
