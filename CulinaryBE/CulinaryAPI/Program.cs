using BusinessObject.AppDbContext;
using BusinessObject.Models.Dto;
using CulinaryAPI.Core;
using CulinaryAPI.Middleware.Authentication;
using CulinaryAPI.Middleware.ExceptionHelper;
using CulinaryAPI.Middleware.JwtCookie;
using CulinaryAPI.SignalRHub;
using Elastic.Clients.Elasticsearch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServiceObject.Configurations;
using Elastic.Transport;


var builder = WebApplication.CreateBuilder(args);


//Coanfig Supabase
builder.Services.AddDbContext<CulinaryContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection"), npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
        npgsqlOptions.CommandTimeout(60); // Tăng thời gian chờ
        npgsqlOptions.UseVector();
        npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    }));

//Config Elasticsearch
builder.Services.AddSingleton(sp =>
{
    var config = builder.Configuration.GetSection("Elasticsearch");
    var cloudId = config["CloudId"];
    var apiKey = config["ApiKey"];

    var settings = new ElasticsearchClientSettings(cloudId, new ApiKey(apiKey))
                        .DefaultIndex("products");

    return new ElasticsearchClient(settings);
});

//Config Email setting
builder.Services.Configure<EmailSetting>(
    builder.Configuration.GetSection("EmailSettings"));

//Config Serilog
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services);
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Config JWT
builder.Services.AddJwtAuthentication();

// Configure Swagger with JWT
builder.Services.AddSwaggerWithJwt();

//Add Memory Cache
builder.Services.AddMemoryCache();

// Configure Dependency Injection
builder.Services.Configure();

//SignalR
builder.Services.AddSignalR();

//Add Authentication
builder.Services.AddAuthorization();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

//Config auto mapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

//Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});


var app = builder.Build();

//Add exception and logging handling middleware
app.UseSerilogRequestLogging();
app.UseExceptionHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

//Auto set token into header when recieve request
app.UseMiddleware<JwtCookieMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<SignalRServer>("/hub");

app.MapControllers();

//Close and flush Serilog on application stop
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

app.Run();
