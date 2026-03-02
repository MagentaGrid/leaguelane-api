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
            group.MapGet("", GetAllJobConfiguration).WithName("job-configurations").Produces<object>(200);

            group.MapGet("{id:int}", GetJobConfiguration).WithName("job-configuration").Produces<object>(200);

            group.MapPost("", CreateJobConfiguration).WithName("job-configuration-create").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("{id:int}", UpdateJobConfiguration).WithName("job-configuration-update").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        /// <summary>
        /// Get all job configurations.
        /// Returns all job configurations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetAllJobConfiguration(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetAllJobConfigurationQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Get job configuration by id.
        /// Returns a job configuration by id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetJobConfiguration(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetJobConfigurationQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Create job configuration.
        /// Creates a new job configuration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> CreateJobConfiguration(ISender sender, JobConfigurationsDto request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateJobConfigurationCommand(request), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Update job configuration.
        /// Updates an existing job configuration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateJobConfiguration(ISender sender, JobConfigurationsDto request, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateJobConfigurationCommand(id, request), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
