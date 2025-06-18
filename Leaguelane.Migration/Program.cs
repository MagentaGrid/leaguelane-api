using Leaguelane.Migration;
using Leaguelane.Persistence.Context;
using OpenTelemetry.Trace;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

builder.Services.AddOpenTelemetry()
   .WithTracing(tracing =>
   {
       tracing.AddAspNetCoreInstrumentation()
           // Uncomment the following line to enable gRPC instrumentation (requires the OpenTelemetry.Instrumentation.GrpcNetClient package)
           //.AddGrpcClientInstrumentation()
           .AddHttpClientInstrumentation();
   });

builder.AddSqlServerDbContext<LeaguelaneDbContext>("LeaguelaneConnection");

var host = builder.Build();
host.Run();
