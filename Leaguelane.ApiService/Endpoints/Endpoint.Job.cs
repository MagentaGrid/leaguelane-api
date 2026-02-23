using Leaguelane.ApiService.Feature;
using Leaguelane.ApiService.Handlers;
using Leaguelane.Constants.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class JobEndpoint
    {
        public static RouteGroupBuilder AddJobRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetJobs).WithName("job-list")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));
            return group;
        }

        public static async Task<IResult> GetJobs([FromServices] IJobSchedulerFeatureService jobSchedulerFeatureService, CancellationToken cancellationToken)
        {
            var result = await jobSchedulerFeatureService.GetAllJobScheduler(cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> ScheduleSeason(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SeasonScheduerCommand(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> ScheduleCountry(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CountrySchedulerCommand(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> ScheduleLeague(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new LeagueSchedulerCommand(), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
