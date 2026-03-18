using Leaguelane.ApiService.Feature;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class TeamEndpoint
    {
        public static RouteGroupBuilder AddTeamRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetTeams).WithName("teams")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("", Update).WithName("team-update")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("disable", Disable).WithName("team-disable")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("enable", Enable).WithName("team-enable")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }
        public static async Task<IResult> GetTeams([FromServices] ITeamFeatureService teamFeatureService, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null, [FromQuery] string status = "All", CancellationToken cancellationToken = default)
        {
            var result = await teamFeatureService.GetAllTeams(page, pageSize, search, status, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> Update([FromServices] ITeamFeatureService teamFeatureService, [FromBody] TeamUpdateDto teamUpdateDto, CancellationToken cancellationToken)
        {
            var result = await teamFeatureService.UpdateTeam(teamUpdateDto, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> Disable([FromServices] ITeamFeatureService teamFeatureService, [FromQuery] int teamId, CancellationToken cancellationToken)
        {
            var result = await teamFeatureService.DisableTeam(teamId, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> Enable([FromServices] ITeamFeatureService teamFeatureService, [FromQuery] int teamId, CancellationToken cancellationToken)
        {
            var result = await teamFeatureService.EnableTeam(teamId, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
