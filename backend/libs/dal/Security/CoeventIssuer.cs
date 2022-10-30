namespace CoEvent.DAL.Security;

/// <summary>
/// 
/// </summary>
public static class CoEventIssuer
{
  /// <summary>
  /// 
  /// </summary>
  public const string Issuer = "coevent";

  /// <summary>
  /// 
  /// </summary>
  public const string OriginalIssuer = "coevent";

  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public static string Account(long id)
  {
    return $"account:{id}";
  }
}
