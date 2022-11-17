using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoEvent.Mail;

/// <summary>
/// ServiceCollectionExtensions static class, provides extension methods for IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
  /// <summary>
  /// Add mail to the service collection.
  /// </summary>
  /// <param name="services"></param>
  /// <param name="config"></param>
  /// <returns></returns>
  public static IServiceCollection AddMail(this IServiceCollection services, IConfiguration config)
  {
    services.AddOptions<MailOptions>().Bind(config.GetSection("Mail"));
    return services.AddSingleton<MailClient>();
  }
}
