using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoEvent.DAL.Extensions;

namespace CoEvent.UoW;

/// <summary>
/// ServiceCollectionExtensions static class, provides extension methods for IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
  #region Methods
  /// <summary>
  /// Add CoEvent units of work to the service collection.
  /// </summary>
  /// <param name="services"></param>
  /// <param name="config"></param>
  /// <param name="options"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  public static IServiceCollection AddCoEventUoW(this IServiceCollection services, IConfiguration config, Action<DbContextOptionsBuilder>? options = null)
  {
    services.AddCoEventContext(config, options);

    // Find all the configuration classes.
    var assembly = typeof(BaseService).Assembly;
    var type = typeof(IBaseService);
    var tnoServiceTypes = assembly.GetTypes().Where(t => !t.IsAbstract && t.IsClass && t.GetInterfaces().Any(i => i.Name.Equals(type.Name)));
    foreach (var serviceType in tnoServiceTypes)
    {
      var sInterface = serviceType.GetInterface($"I{serviceType.Name}") ?? throw new InvalidOperationException($"Service type '{serviceType.Name}' is missing its interface.");
      services.AddScoped(sInterface, serviceType);
    }

    return services;
  }
  #endregion
}
