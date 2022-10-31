using System.Security.Claims;

namespace CoEvent.UoW;

/// <summary>
/// 
/// </summary>
public interface IBaseService
{
  /// <summary>
  /// 
  /// </summary>
  ClaimsPrincipal Principal { get; }

  /// <summary>
  /// 
  /// </summary>
  IServiceProvider Services { get; }
}
