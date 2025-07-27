using Leaguelane.Api.Handlers;
using Leaguelane.ApiService.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;

namespace Leaguelane.ApiService.Endpoints
{
    public static class JobConfigurationEndpoint
    {
        public static RouteGroupBuilder AddJobConfigurationRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetAllJobConfiguration).WithName("job-configurations");

            group.MapGet("{id:int}", GetJobConfiguration).WithName("job-configuration");

            group.MapPost("", CreateJobConfiguration).WithName("job-configuration-create")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("{id:int}", UpdateJobConfiguration).WithName("job-configuration-update")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        public static async Task<IResult> GetAllJobConfiguration(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetAllJobConfigurationQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetJobConfiguration(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetJobConfigurationQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreateJobConfiguration(ISender sender, JobConfigurationsDto request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateJobConfigurationCommand(request), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateJobConfiguration(ISender sender, JobConfigurationsDto request, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateJobConfigurationCommand(id, request), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
