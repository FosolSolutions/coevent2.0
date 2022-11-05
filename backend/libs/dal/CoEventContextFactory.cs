namespace CoEvent.DAL;

using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

/// <summary>
/// CoEventContextFactory class, provides a way to initialize the CoEventContext to run database migrations.
/// </summary>
public class CoEventContextFactory : IDesignTimeDbContextFactory<CoEventContext>
{
  #region Variables
  private readonly ILogger<CoEventContextFactory> _logger;
  #endregion

  #region Constructors
  /// <summary>
  /// Creates a new instance of a CoEventContextFactory object.
  /// </summary>
  public CoEventContextFactory()
  {
    var loggerFactory = LoggerFactory.Create(builder =>
    {
      builder
              .AddFilter("Microsoft", LogLevel.Warning)
              .AddFilter("System", LogLevel.Warning)
              .AddFilter("CoEvent", LogLevel.Debug)
              .AddConsole();
      // .AddEventLog();
    });
    _logger = loggerFactory.CreateLogger<CoEventContextFactory>();
  }
  #endregion

  #region Methods
  /// <summary>
  /// 
  /// </summary>
  /// <param name="args"></param>
  /// <returns></returns>
  /// <exception cref="Exception"></exception>
  public CoEventContext CreateDbContext(string[] args)
  {
    DotNetEnv.Env.Load();
    string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

    IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("connectionstrings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"connectionstrings.{environment}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
    // Here we create the DbContextOptionsBuilder manually.
    var builder = new DbContextOptionsBuilder<CoEventContext>();

    // Build connection string. This requires that you have a connectionstring in the appsettings.json
    var cs = configuration.GetConnectionString("DefaultConnection");
    var sqlBuilder = new SqlConnectionStringBuilder(cs);
    if (String.IsNullOrWhiteSpace(sqlBuilder.DataSource) && !String.IsNullOrWhiteSpace(configuration["DB_ADDR"]))
      sqlBuilder.DataSource = configuration["DB_ADDR"];
    sqlBuilder.InitialCatalog = !String.IsNullOrWhiteSpace(sqlBuilder.InitialCatalog) ? sqlBuilder.InitialCatalog : configuration["DB_NAME"];
    sqlBuilder.UserID = !String.IsNullOrWhiteSpace(sqlBuilder.UserID) ? sqlBuilder.UserID : configuration["DB_USER"];
    sqlBuilder.Password = !String.IsNullOrWhiteSpace(sqlBuilder.Password) ? sqlBuilder.Password : configuration["DB_PASSWORD"];

    FactorySettings.DefaultPassword = !String.IsNullOrWhiteSpace(configuration["DEFAULT_PASSWORD"]) ? configuration["DEFAULT_PASSWORD"] : throw new Exception("Configuration 'DEFAULT_PASSWORD' is required.");
    if (Int32.TryParse(configuration["SALT_LENGTH"], out int saltLength))
    {
      FactorySettings.SaltLength = saltLength;
    }

    var optionsBuilder = new DbContextOptionsBuilder<CoEventContext>();
    optionsBuilder.UseSqlServer(sqlBuilder.ConnectionString, options =>
    {
      options.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
    });

    var serializerOptions = new JsonSerializerOptions()
    {
      DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      WriteIndented = true
    };
    var optionsSerializer = Microsoft.Extensions.Options.Options.Create(serializerOptions);

    _logger.LogInformation("Context created for {DataSource}", sqlBuilder.DataSource);
    return new CoEventContext(optionsBuilder.Options, new HttpContextAccessor(), optionsSerializer);
  }
  #endregion
}
