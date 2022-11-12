using System.Text.Json.Serialization;
using CoEvent.API.Extensions;
using CoEvent.API.Middleware;
using CoEvent.Core.Extensions;
using CoEvent.Swagger;
using CoEvent.UoW;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var env = builder.Environment;

// *****************************************
// Services 
// *****************************************
builder.Services.AddSingleton(config);
builder.Services.AddSingleton(env);

builder.Services.AddJsonSerializerOptions(config);
builder.Services.AddControllers()
  .AddJsonOptions(options =>
  {
    config.CreateJsonSerializerOptions(options.JsonSerializerOptions);
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
  });
builder.Services.AddRouting(options =>
{
  options.LowercaseUrls = true;
});
builder.Services.AddSwaggerDocs();
builder.Services.AddCoEventAuthentication(config);
builder.Services.AddCoEventUoW(config);

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
  options.ForwardedHeaders = ForwardedHeaders.All;
  options.AllowedHosts = config.GetValue<string>("AllowedHosts")?.Split(';').ToList() ?? new List<string>();
});
builder.Services.AddCors(options =>
{
  var withOrigins = config.GetSection("Cors:WithOrigins").Value?.Split(" ") ?? Array.Empty<string>();
  if (withOrigins.Any())
  {
    options.AddPolicy(
      name: "allowedOrigins",
      builder =>
      {
        builder
          .WithOrigins(withOrigins)
          .AllowAnyHeader()
          .AllowAnyMethod(); ;
      });
  }
});

// *****************************************
// Application 
// *****************************************
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwaggerDocs(config);
}

app.UsePathBase(config.GetValue<string>("BaseUrl"));
app.UseForwardedHeaders();

app.UseMiddleware(typeof(ErrorHandlingMiddleware));
app.UseMiddleware(typeof(ResponseTimeMiddleware));

// app.UseHttpsRedirection();
app.UseCors("allowedOrigins");
app.UseStaticFiles();

app.UseMiddleware(typeof(LogRequestMiddleware));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
