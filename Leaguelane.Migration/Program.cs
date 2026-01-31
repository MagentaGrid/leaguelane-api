using Leaguelane.Migration;
using Leaguelane.Persistence.Context;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();


builder.AddSqlServerDbContext<LeaguelaneDbContext>("LeaguelaneConnection");

var host = builder.Build();
host.Run();
