using Leaguelane.Api.Configurations;
using Leaguelane.ApiService.Endpoints;
using Leaguelane.Persistence.Context;
using Azure.Storage.Blobs;
using Leaguelane.Scheduler;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin() // or use .WithOrigins("http://localhost:60467") for stricter setup
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

//sql connection
builder.AddSqlServerDbContext<LeaguelaneDbContext>("LeaguelaneConnection");

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

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

app.MapEndpoints();

app.MapDefaultEndpoints();

app.Run();