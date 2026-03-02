using Leaguelane.ApiService.Handlers;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Leaguelane.ApiService.Endpoints
{
    public static class SportEndpoint
    {
        public static RouteGroupBuilder AddSportRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetSports).WithName("sports").Produces<object>(StatusCodes.Status200OK);
            group.MapGet("{id:int}", GetSportById).WithName("sport-by-id").Produces<object>(StatusCodes.Status200OK);
            group.MapPost("", CreateSport).WithName("sport-create").Produces<object>(StatusCodes.Status200OK);
            group.MapPut("{id:int}", UpdateSport).WithName("sport-update").Produces<object>(StatusCodes.Status200OK);
            return group;
        }

        /// <summary>
        /// Get all sports.
        /// Returns a list of sports.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetSports(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetAllSportsQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Get sport by id.
        /// Returns details for a sport specified by id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetSportById(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetSportByIdQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Create sport.
        /// Creates a new sport.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="sport"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> CreateSport(ISender sender, SportDto sport, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateSportCommand(sport), cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Update sport.
        /// Updates an existing sport by id.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="sport"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> UpdateSport(ISender sender, SportDto sport, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateSportCommand(sport, id), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
