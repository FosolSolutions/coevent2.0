using System.Configuration;
using System.Security.Claims;
using System.Text;
using CoEvent.API.Authentication;
using CoEvent.API.Config;
using CoEvent.Core.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace CoEvent.API.Extensions;

/// <summary>
/// AuthenticationExtensions static class, provides extensions for IServiceCollection.
/// </summary>
public static class AuthenticationExtensions
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="services"></param>
  /// <param name="configuration"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  public static IServiceCollection AddCoEventAuthentication(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddHttpContextAccessor();
    services.AddTransient(s => s.GetService<IHttpContextAccessor>()?.HttpContext?.User ?? new ClaimsPrincipal());
    services.AddSingleton<IHashPassword, HashPassword>();
    services.AddScoped<IAuthenticator, Authenticator>();

    var config = configuration.GetSection("Authentication").Get<CoEventAuthenticationOptions>() ?? throw new ConfigurationErrorsException("Authentication section is required.");
    services.Configure<CoEventAuthenticationOptions>(configuration.GetSection("Authentication"));
    services.AddAuthentication(options =>
      {
        options.DefaultScheme = "JWT_OR_COOKIE";
        options.DefaultChallengeScheme = "JWT_OR_COOKIE";
        options.RequireAuthenticatedSignIn = false;
      })
      .AddCookie("Cookies", options =>
      {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/forbidden";
        options.Events.OnRedirectToLogin = ctx =>
        {
          ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
          return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = ctx =>
        {
          ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
          return Task.CompletedTask;
        };
      })
      .AddJwtBearer("Bearer", options =>
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
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.PrivateKey ?? throw new ConfigurationErrorsException("Authentication:PrivateKey configuration is required."))),
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
      })
      .AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
      {
        options.ForwardDefaultSelector = context =>
        {
          string? authorization = context.Request.Headers[HeaderNames.Authorization];
          if (!String.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
            return "Bearer";
          return "Cookies";
        };
      });

    return services;
  }
}
