using Leaguelane.Models.Dtos;
using MediatR;
using Leaguelane.ApiService.Handlers;

namespace Leaguelane.ApiService.Endpoints
{
    public static class OddsEndpoint
    {
        public static RouteGroupBuilder AddOddsRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetOdds).WithName("odds-list");
            group.MapGet("all", GetAllOdds).WithName("odds-list-all");
            group.MapGet("{id:int}", GetOddsById).WithName("odds-by-id");
            group.MapPut("{id:int}", UpdateOdds).WithName("odds-update");
            group.MapPut("{id:int}/delete", SoftDeleteOdds).WithName("odds-delete");
            group.MapPut("{id:int}/restore", RestoreOdds).WithName("odds-restore");
            group.MapGet("deleted", GetDeletedOdds).WithName("odds-deleted");
            return group;
        }

        public static async Task<IResult> GetOdds(ISender sender, int? fixtureId, int? bookmakerId, string? market, int skip = 0, int take = 20, CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new GetOddsQuery(fixtureId, bookmakerId, market, skip, take), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetAllOdds(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetAllOddsQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetOddsById(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetOddsByIdQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateOdds(ISender sender, int id, OddsDto odds, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateOddsCommand(id, odds), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> SoftDeleteOdds(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SoftDeleteOddsCommand(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> RestoreOdds(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new RestoreOddsCommand(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetDeletedOdds(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetDeletedOddsQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
