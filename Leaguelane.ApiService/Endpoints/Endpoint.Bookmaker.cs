using Leaguelane.ApiService.Feature;
using Leaguelane.Constants.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class BookmakerEndpoint
    {
        public static RouteGroupBuilder AddBookmakerRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetAll).WithName("bookmaker-list")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("enable", Enable).WithName("bookmaker-enble")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("disable", Disable).WithName("bookmaker-disable")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }
        public static async Task<IResult> GetAll([FromServices] IBookmakerFeatureService bookmakerFeatureService, [FromQuery]int page = 1, [FromQuery]int pageSize = 10, [FromQuery]string search = "", CancellationToken cancellationToken = default)
        {
            var result = await bookmakerFeatureService.GetAllBookmakers(page, pageSize, search, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> Enable([FromServices] IBookmakerFeatureService bookmakerFeatureService, [FromQuery]int bookmakerId, CancellationToken cancellationToken)
        {
            var result = await bookmakerFeatureService.EnableBookmaker(bookmakerId, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> Disable([FromServices] IBookmakerFeatureService bookmakerFeatureService, [FromQuery]int bookmakerId, CancellationToken cancellationToken)
        {
            var result = await bookmakerFeatureService.DisableBookmaker(bookmakerId, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
