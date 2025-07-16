using Leaguelane.ApiService.Handlers;
using MediatR;

namespace Leaguelane.ApiService.Endpoints
{
    public static class Endpoint
    {
        public static RouteGroupBuilder AddSchedulerRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("season", ScheduleSeason).WithName("schedule-season");
            group.MapGet("country", ScheduleCountry).WithName("schedule-country");
            group.MapGet("league", ScheduleLeague).WithName("schedule-league");
            return group;
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
