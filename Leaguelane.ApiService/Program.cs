using Leaguelane.Api.Configurations;
using Leaguelane.ApiService.Endpoints;
using Leaguelane.ApiService.Extensions;
using Leaguelane.Persistence.Context;
using Leaguelane.Scheduler;
using Leaguelane.ApiService.Middlewears;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendLocalhost", policy =>
    {
        policy.AllowAnyOrigin() // <- WARNING: use only in dev
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo
    {
        Title = "LL API",
        Version = "V1"
    });
});


//sql connection
//builder.AddSqlServerDbContext<LeaguelaneDbContext>("LeaguelaneConnection");

builder.Services.AddDbContext<LeaguelaneDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("LeaguelaneConnection")));

builder.Services.AddDbContext<LoggingDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("LoggingConnection")));

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddScheduler();

builder.RegisterServices();

builder.JwtConfiguration();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<RequestResponseLoggingMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "LL API V1");
});

app.MapEndpoints();

await app.EnsureDatabaseCreated();
await app.EnsureLoggingDatabaseCreated();

app.Run();