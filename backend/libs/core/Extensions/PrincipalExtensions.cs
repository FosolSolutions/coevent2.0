namespace CoEvent.Core.Extensions;

using System.Security.Claims;
using System.Security.Principal;

/// <summary>
/// PrincipalExtensions static class, provides extension methods for IPrincipal objects.
/// </summary>
public static class PrincipalExtensions
{
  /// <summary>
  /// Get the claim for the specified 'type' if it exists.
  /// </summary>
  /// <param name="principal"></param>
  /// <param name="type"></param>
  /// <returns></returns>
  public static Claim? GetClaim(this IPrincipal principal, string type)
  {
    return (principal as ClaimsPrincipal)?.Claims.FirstOrDefault(c => c.Type == type);
  }
}
