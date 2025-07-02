using Leaguelane.ApiService.Handlers;
using Leaguelane.Models.Dtos;
using MediatR;

namespace Leaguelane.ApiService.Endpoints
{
    public static class SportEndpoint
    {
        public static RouteGroupBuilder AddSportRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetSports).WithName("sports");
            group.MapGet("{id:int}", GetSportById).WithName("sport-by-id");
            group.MapPost("", CreateSport).WithName("sport-create");
            group.MapPut("{id:int}", UpdateSport).WithName("sport-update");
            return group;
        }

        public static async Task<IResult> GetSports(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetAllSportsQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetSportById(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetSportByIdQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreateSport(ISender sender, SportDto sport, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateSportCommand(sport), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateSport(ISender sender, SportDto sport, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateSportCommand(sport, id), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
