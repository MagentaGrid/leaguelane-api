using Leaguelane.Api.Handlers;
using Leaguelane.ApiService.Feature;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class LeagueEndpoint
    {
        public static RouteGroupBuilder AddLeagueRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetLeagues).WithName("leagues")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("", Update).WithName("league-update")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("disable", Disable).WithName("league-disable")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("enable", Enable).WithName("league-enable")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }
        public static async Task<IResult> GetLeagues([FromServices] ILeagueFeatureService leagueFeatureService, [FromQuery]int page = 1, [FromQuery]int pageSize = 10, [FromQuery]string? search = null, CancellationToken cancellationToken = default)
        {
            var result = await leagueFeatureService.GetAllLeagues(page, pageSize, search, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> Update([FromServices] ILeagueFeatureService leagueFeatureService, [FromBody] UpdateLeagueRequestDto updateLeagueRequestDto, CancellationToken cancellationToken)
        {
            var result = await leagueFeatureService.UpdateLeague(updateLeagueRequestDto, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> Disable([FromServices] ILeagueFeatureService leagueFeatureService, [FromQuery] int leagueId, CancellationToken cancellationToken)
        {
            var result = await leagueFeatureService.DisableLeague(leagueId, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> Enable([FromServices] ILeagueFeatureService leagueFeatureService, [FromQuery] int leagueId, CancellationToken cancellationToken)
        {
            var result = await leagueFeatureService.EnableLeague(leagueId, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
