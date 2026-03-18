using Leaguelane.ApiService.Feature;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class PredictionsEndpoints
    {
        public static RouteGroupBuilder AddPredictionRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("/featured", GetFeaturedPredictions).WithName("predictions-featured").Produces<object>(200);
            group.MapGet("", GetPredictions).WithName("predictions-list").Produces<object>(200);
            group.MapGet("/{fixtureId:int}", GetPredictionDetail).WithName("predictions-detail").Produces<object>(200);

            return group;
        }

        /// <summary>
        /// Get featured predictions.
        /// Returns featured predictions for display on home/featured section.
        /// </summary>
        /// <param name="fixtureFeatureService"></param>
        /// <param name="count"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetFeaturedPredictions([FromServices] IFixtureFeatureService fixtureFeatureService, int count = 6, CancellationToken cancellationToken = default)
        {
            var result = await fixtureFeatureService.GetFeaturedPredictions(count, cancellationToken);
            return TypedResults.Ok(result);
        }



        /// <summary>
        /// Get predictions (paged).
        /// Returns predictions grouped by league. Query parameters: league, page, pageSize.
        /// </summary>
        /// <param name="fixtureFeatureService"></param>
        /// <param name="league"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetPredictions([FromServices] IFixtureFeatureService fixtureFeatureService, string league = "all", int page = 1, int pageSize = 6, CancellationToken cancellationToken = default)
        {
            var result = await fixtureFeatureService.GetPredictions(league, page, pageSize, cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Get prediction detail for a specific fixture.
        /// Returns full match detail including teams, form, top markets, and insight.
        /// </summary>
        public static async Task<IResult> GetPredictionDetail([FromServices] IFixtureFeatureService fixtureFeatureService, int fixtureId, CancellationToken cancellationToken = default)
        {
            var result = await fixtureFeatureService.GetPredictionDetail(fixtureId, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
