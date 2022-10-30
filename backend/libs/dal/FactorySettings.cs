namespace CoEvent.DAL;

/// <summary>
/// FactorySettings static class, provides a way to make factory settings accessible during database migrations.
/// </summary>
public static class FactorySettings
{
  /// <summary>
  /// get/set - The default password to apply to users in the seed scripts.
  /// </summary>
  public static string DefaultPassword { get; set; } = String.Empty;

  /// <summary>
  /// get/set - The length of the salt used in password hashing.
  /// </summary>
  public static int SaltLength { get; set; } = 50;
}
