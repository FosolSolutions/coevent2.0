using System.Text.Json.Serialization;
using CoEvent.API.Config;
using CoEvent.API.Extensions;
using CoEvent.API.Middleware;
using CoEvent.Core.Extensions;
using CoEvent.Swagger;

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
builder.Services.AddSwaggerDocs();
builder.Services.AddCoEventAuthentication(config);

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

app.UseMiddleware(typeof(ErrorHandlingMiddleware));
app.UseMiddleware(typeof(ResponseTimeMiddleware));
// app.UseHttpsRedirection();

app.UseMiddleware(typeof(LogRequestMiddleware));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
