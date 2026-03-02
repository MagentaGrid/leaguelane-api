using Leaguelane.Models.Dtos;
using MediatR;
using Leaguelane.ApiService.Handlers;
using Microsoft.AspNetCore.Mvc;
using Leaguelane.ApiService.Feature;
using Leaguelane.Constants.Enums;

namespace Leaguelane.ApiService.Endpoints
{
    public static class OddsEndpoint
    {
        public static RouteGroupBuilder AddOddsRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetOddsForDropdown).WithName("odds-list").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("bookmakers", GetBookmakersForDropdown).WithName("odds-bookmakers").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("bets", GetBetsForDropdown).WithName("odds-bets").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        /// <summary>
        /// Get odds for dropdown (admin).
        /// Returns odds filtered by bet/bookmaker/fixture.
        /// </summary>
        /// <param name="oddFeatureService"></param>
        /// <param name="betId"></param>
        /// <param name="bookmakerId"></param>
        /// <param name="fixtureId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetOddsForDropdown([FromServices]IOddFeatureService oddFeatureService, [FromQuery] int betId, [FromQuery] int bookmakerId, [FromQuery] int fixtureId, CancellationToken cancellationToken = default)
        {
            var result = await oddFeatureService.GetAllOddsForBetAndBookmaker(betId, bookmakerId, fixtureId, cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Get bookmakers for dropdown (admin).
        /// Returns bookmakers.
        /// </summary>
        /// <param name="oddFeatureService"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetBookmakersForDropdown([FromServices] IOddFeatureService oddFeatureService, CancellationToken cancellationToken = default)
        {
            var result = await oddFeatureService.GetAllBookmakers(cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Get bets for dropdown (admin).
        /// Returns bets.
        /// </summary>
        /// <param name="oddFeatureService"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetBetsForDropdown([FromServices] IOddFeatureService oddFeatureService, CancellationToken cancellationToken = default)
        {
            var result = await oddFeatureService.GetAllBets(cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
