namespace CoEvent.API.Config;

using CoEvent.Core.Extensions;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// 
/// </summary>
public static class JsonSerializerExtensions
{
  /// <summary>
  /// 
  /// </summary>
  public static IServiceCollection AddJsonSerializerOptions(this IServiceCollection services, IConfiguration configuration)
  {
    var jsonSerializerOptions = configuration.CreateJsonSerializerOptions();
    services.Configure<JsonSerializerOptions>(options =>
    {
      options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      options.PropertyNameCaseInsensitive = jsonSerializerOptions.PropertyNameCaseInsensitive;
      options.PropertyNamingPolicy = jsonSerializerOptions.PropertyNamingPolicy;
      options.WriteIndented = jsonSerializerOptions.WriteIndented;
      options.Converters.Add(new JsonStringEnumConverter());
    });
    return services;
  }
}
