using System.Text;
using CoEvent.API.Authentication;
using CoEvent.API.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CoEvent.API.Extensions;

/// <summary>
/// 
/// </summary>
public static class AuthenticationExtensions
{
  /// <summary>
  /// 
  /// </summary>
  public static IServiceCollection AddCoEventAuthentication(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<IAuthenticator, Authenticator>();
    services.Configure<CoEventAuthenticationOptions>(configuration.GetSection("Authentication"));
    var config = configuration.GetSection("Authentication").Get<CoEventAuthenticationOptions>();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.RequireHttpsMetadata = true;
          options.SaveToken = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config.Issuer,
            ValidAudience = config.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.PrivateKey ?? throw new InvalidOperationException("Authentication:PrivateKey configuration is required."))),
            ClockSkew = TimeSpan.Zero
          };
          //options.Events = new JwtBearerEvents()
          //{
          //    OnMessageReceived = context =>
          //    {
          //        context.Token = context.Request.Cookies[config.Cookie.Name];
          //        return Task.CompletedTask;
          //    }
          //};
        });

    return services;
  }
}
