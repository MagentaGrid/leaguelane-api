using Aspire.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

// ? Add SQL Server using Aspire's built-in container support
var sql = builder.AddSqlServer("sqlserver", port: 1433)
    .WithEnvironment("ACCEPT_EULA", "Y")
    .WithEnvironment("MSSQL_SA_PASSWORD", "gqKkm93c5rKmr{E{7ZpQ12")
    .AddDatabase("LeaguelaneData");

// ? Add Redis
var cache = builder.AddRedis("cache");

// ? Add the DB migration project
var LeaguelaneMigration = builder.AddProject<Projects.Leaguelane_Migration>("Leaguelanemigration")
    .WaitForCompletion(sql)
    .WithReference(sql);

// ? Add the API service, wait for DB + migration
var apiService = builder.AddProject<Projects.Leaguelane_ApiService>("apiservice")
    .WithReference(sql)
    .WaitForCompletion(LeaguelaneMigration);

// ? Add Next.js frontend via Dockerfile
var frontend = builder.AddDockerfile("web-app", "../web-app")
    .WaitFor(apiService)
    .WithReference(apiService)
    .WithHttpEndpoint(targetPort: 3000, env: "PORT")
    .WithExternalHttpEndpoints();

// ? Add CORS (optional but good for local dev)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Build().Run();
