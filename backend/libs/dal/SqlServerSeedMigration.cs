namespace CoEvent.DAL;

/// <summary>
/// SqlServerSeedMigration abstract class, provides seed migration for SQL Server.
/// </summary>
public abstract class SqlServerSeedMigration : SeedMigration
{
  /// <summary>
  /// Print a message to the SQL output.
  /// </summary>
  /// <param name="message"></param>
  /// <returns></returns>
  protected override string PrintMessage(string message)
  {
    return base.PrintMessage($"PRINT '{message}'");
  }
}
