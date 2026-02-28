using Leaguelane.ApiService.Feature;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class PredictionsEndpoints
    {
        public static RouteGroupBuilder AddPredictionRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/featured", GetFeaturedPredictions).WithName("predictions-featured");

            return group;
        }

        public static async Task<IResult> GetFeaturedPredictions([FromServices] IFixtureFeatureService fixtureFeatureService, int count = 6, CancellationToken cancellationToken = default)
        {
            var result = await fixtureFeatureService.GetFeaturedPredictions(count, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
