using Leaguelane.ApiService.Handlers;
using Leaguelane.Models.Dtos;
using MediatR;

namespace Leaguelane.ApiService.Endpoints
{
    public static class FixtureEndpoint
    {
        public static RouteGroupBuilder AddFixtureRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetFixtures).WithName("fixtures");
            group.MapGet("{id:int}", GetFixtureById).WithName("fixture-by-id");
            group.MapPut("{id:int}", UpdateFixture).WithName("fixture-update");
            group.MapDelete("{id:int}", DeleteFixture).WithName("fixture-delete");
            group.MapPut("{id:int}/rank", SetRank).WithName("fixture-set-rank");
            group.MapGet("latest", GetLatestFixtures).WithName("fixtures-latest");
            return group;
        }

        public static async Task<IResult> GetFixtures(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetAllFixturesQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetFixtureById(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetFixtureByIdQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateFixture(ISender sender, int id, FixtureDto fixture, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateFixtureCommand(id, fixture), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> DeleteFixture(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SoftDeleteFixtureCommand(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> SetRank(ISender sender, int id, SetRankRequestDto request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new SetRankCommand(id, request.Rank), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetLatestFixtures(ISender sender, int page, int pageSize, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetLatestFixtureQuery(page, pageSize), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
