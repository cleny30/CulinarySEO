using CulinaryAPI.Core;
using CulinaryAPI.Middleware.Authentication;
using CulinaryAPI.SignalRHub;
using Microsoft.AspNetCore.Authorization;
using ServiceObject.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Config JWT
builder.Services.AddJwtAuthentication();

// Configure Swagger with JWT
builder.Services.AddSwaggerWithJwt();

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
        policy.WithOrigins(
                       "http://localhost:5173"
                     )
                     .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<SignalRServer>("/hub");

app.MapControllers();

app.Run();
