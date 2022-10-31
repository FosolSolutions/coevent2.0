using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using CoEvent.DAL;

namespace CoEvent.UoW;

/// <summary>
/// ServiceCollectionExtensions static class, provides extension methods for IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
  /// <summary>
  /// Add a PostgreSQL DbContext to the service collection.
  /// </summary>
  /// <param name="services"></param>
  /// <param name="connectionString"></param>
  /// <param name="env"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentException"></exception>
  public static IServiceCollection AddCoEventContext(this IServiceCollection services, string connectionString, IWebHostEnvironment env)
  {
    if (String.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("Argument is required and cannot be null, empty or whitespace.", nameof(connectionString));

    services.AddDbContext<CoEventContext>(options =>
    {
      var db = options.UseSqlServer(connectionString, options =>
            {
              options.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds);
            });
      options.EnableSensitiveDataLogging(env.IsDevelopment());
      options.EnableDetailedErrors(env.IsDevelopment());
      if (env.IsDevelopment())
      {
        var debugLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole().AddDebug(); });
        db.UseLoggerFactory(debugLoggerFactory);
      }
    });

    return services;
  }

  /// <summary>
  /// Add a PostgreSQL DbContext to the service collection.
  /// </summary>
  /// <param name="services"></param>
  /// <param name="config"></param>
  /// <param name="env"></param>
  /// <returns></returns>
  public static IServiceCollection AddCoEventContext(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
  {
    var postgresBuilder = new SqlConnectionStringBuilder(config["ConnectionStrings:CoEvent"])
    {
      UserID = config["DB_USER"],
      Password = config["DB_PASSWORD"]
    };
    return services.AddCoEventContext(postgresBuilder.ConnectionString, env);
  }

  /// <summary>
  /// Add a PostgreSQL DbContext to the service collection and all the related services.
  /// </summary>
  /// <param name="services"></param>
  /// <param name="config"></param>
  /// <param name="env"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="InvalidOperationException"></exception>
  public static IServiceCollection AddTNOServices(this IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
  {
    if (config == null) throw new ArgumentNullException(nameof(config));

    services.AddCoEventContext(config, env);

    // Find all the configuration classes.
    var assembly = typeof(BaseService).Assembly;
    var type = typeof(IBaseService);
    var tnoServiceTypes = assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass && t.GetInterfaces().Any(i => i.Name.Equals(type.Name)));
    foreach (var serviceType in tnoServiceTypes)
    {
      var sinterface = serviceType.GetInterface($"I{serviceType.Name}") ?? throw new InvalidOperationException($"Service type '{serviceType.Name}' is missing its interface.");
      services.AddScoped(sinterface, serviceType);
    }

    return services;
  }
}
