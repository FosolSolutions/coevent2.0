using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CoEvent.Swagger;

/// <summary>
/// SwaggerExtensions static class, provides extension methods for IServiceCollection, and IApplicationBuilder.
/// </summary>
public static class SwaggerExtensions
{
  /// <summary>
  /// Add swagger to dependency injection.
  /// </summary>
  /// <param name="services"></param>
  /// <returns></returns>
  public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
  {
    services.AddHttpContextAccessor();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddApiVersioning(options =>
    {
      options.ReportApiVersions = true;
      options.AssumeDefaultVersionWhenUnspecified = true;
      options.ApiVersionReader = new HeaderApiVersionReader("api-version");
      // options.DefaultApiVersion = new ApiVersion(1, 0);
    });
    services.AddVersionedApiExplorer(options =>
    {
      // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
      // note: the specified format code will format the version as "'v'major[.minor][-status]"
      options.GroupNameFormat = "'v'VVV";

      // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
      // can also be used to control the format of the API version in route templates
      options.SubstituteApiVersionInUrl = true;

    });
    services.AddTransient<IConfigureOptions<SwaggerGenOptions>, API.Swagger.Config.ConfigureSwaggerOptions>();
    services.AddEndpointsApiExplorer();

    return services.AddSwaggerGen(options =>
    {
      options.EnableAnnotations(false, true);
      options.CustomSchemaIds(o => o.FullName);
      options.OperationFilter<API.Swagger.Config.SwaggerDefaultValues>();
      options.DocumentFilter<API.Swagger.Config.SwaggerDocumentFilter>();
      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Please enter into field the word 'Bearer' following by space and JWT",
        Type = SecuritySchemeType.ApiKey
      });
      options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
        });

      var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
      options.IncludeXmlComments(xmlPath);
    });
  }

  /// <summary>
  /// Include swagger docs and UI in application.
  /// </summary>
  /// <param name="builder"></param>
  /// <param name="config"></param>
  /// <returns></returns>
  public static IApplicationBuilder UseSwaggerDocs(this WebApplication builder, IConfiguration config)
  {
    // TODO: Secure swagger
    builder.UseSwagger(options =>
    {
      options.RouteTemplate = config.GetValue<string>("Swagger:RouteTemplate");
    });
    builder.UseSwaggerUI(options =>
    {
      var apiVersionProvider = builder.Services.GetRequiredService<IApiVersionDescriptionProvider>();
      foreach (var description in apiVersionProvider.ApiVersionDescriptions)
      {
        options.SwaggerEndpoint(String.Format(config.GetValue<string>("Swagger:EndpointPath"), description.GroupName), description.GroupName);
      }
      options.RoutePrefix = config.GetValue<string>("Swagger:RoutePrefix");
    });

    return builder;
  }
}
