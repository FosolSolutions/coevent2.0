using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoEvent.DAL.Extensions;

/// <summary>
/// ServiceCollectionExtensions static class, provides extension methods for IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
  #region Methods
  /// <summary>
  /// Add CoEventContext to services collection.
  /// Configure the default database.
  /// </summary>
  /// <param name="services"></param>
  /// <param name="config"></param>
  /// <param name="options"></param>
  /// <returns></returns>
  public static IServiceCollection AddCoEventContext(this IServiceCollection services, IConfiguration config, Action<DbContextOptionsBuilder>? options = null)
  {
    return services.AddDbContext<CoEventContext>(opt =>
    {
      // Generate the database connection string.
      var builder = new SqlConnectionStringBuilder(config.GetConnectionString("DefaultConnection"));
      if (String.IsNullOrWhiteSpace(builder.DataSource) && !String.IsNullOrWhiteSpace(config["DB_ADDR"]))
        builder.DataSource = String.IsNullOrWhiteSpace(builder.DataSource) ? config["DB_ADDR"] : builder.DataSource;
      builder.InitialCatalog = String.IsNullOrWhiteSpace(builder.InitialCatalog) ? config["DB_NAME"] : builder.InitialCatalog;
      builder.UserID = String.IsNullOrWhiteSpace(builder.UserID) ? config["DB_USER"] : builder.UserID;
      builder.Password = String.IsNullOrWhiteSpace(builder.Password) ? config["DB_PASSWORD"] : builder.Password;

      var sql = opt.UseSqlServer(builder.ConnectionString, sqlOptions =>
      {
        sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
      });

      if (options == null)
      {
        var debugLoggerFactory = LoggerFactory.Create(builder => { builder.AddDebug(); });
        sql.UseLoggerFactory(debugLoggerFactory);
        opt.EnableSensitiveDataLogging();
        opt.EnableDetailedErrors();
      }

      options?.Invoke(opt);
    });
  }
  #endregion
}
