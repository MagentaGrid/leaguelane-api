using Leaguelane.ApiService.Feature;
using Leaguelane.Constants.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{

    public static class PublicEndpoints
    {
        public static RouteGroupBuilder AddPublicRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/fixtures", GetFixtures).WithName("fixture-list");

            return group;
        }

        public static async Task<IResult> GetFixtures([FromServices] IFixtureFeatureService fixtureFeatureService, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await fixtureFeatureService.GetFixtures(page, pageSize, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
