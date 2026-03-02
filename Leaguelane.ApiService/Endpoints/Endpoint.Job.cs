using Leaguelane.ApiService.Feature;
using Leaguelane.ApiService.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Enums.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class JobEndpoint
    {
        public static RouteGroupBuilder AddJobRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetJobs).WithName("job-list").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("scheduler", Schedule).WithName("job-scheduler").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));
            return group;
        }

        /// <summary>
        /// Get jobs.
        /// Returns configured jobs (admin).
        /// </summary>
        /// <param name="jobSchedulerFeatureService"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetJobs([FromServices] IJobSchedulerFeatureService jobSchedulerFeatureService, CancellationToken cancellationToken)
        {
            var result = await jobSchedulerFeatureService.GetAllJobScheduler(cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Schedule job.
        /// Triggers the specified job scheduler.
        /// </summary>
        /// <param name="jobSchedulerFeatureService"></param>
        /// <param name="job"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> Schedule([FromServices] IJobSchedulerFeatureService jobSchedulerFeatureService, [FromQuery]Jobs job, CancellationToken cancellationToken)
        {
            var result = await jobSchedulerFeatureService.TriggerJob(job, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
