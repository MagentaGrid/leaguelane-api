using Leaguelane.ApiService.Feature;
using Leaguelane.ApiService.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class FixtureEndpoint
    {
        public static RouteGroupBuilder AddFixtureRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetFixtures).WithName("fixtures");
                //.RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("{id:int}", GetFixtureById).WithName("fixture-by-id")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("publish", PublishFixture).WithName("fixture-publish")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("unpublish", UnPublishFixture).WithName("fixture-unpublish")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPost("tip", CreateTip).WithName("fixture-tip-create")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPost("preview", CreatePreview).WithName("fixture-preview-create")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            //group.MapPut("{id:int}/rank", SetRank).WithName("fixture-set-rank");
            //group.MapGet("latest", GetLatestFixtures).WithName("fixtures-latest");
            return group;
        }

        public static async Task<IResult> GetFixtures([FromServices] IFixtureFeatureService fixtureFeatureService, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await fixtureFeatureService.GetFixtures(page, pageSize, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetFixtureById([FromServices] IFixtureFeatureService fixtureFeatureService, int id, CancellationToken cancellationToken)
        {
            var result = await fixtureFeatureService.GetFixtureDetailsById(id, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> PublishFixture([FromServices] IFixtureFeatureService fixtureFeatureService, [FromQuery] int fixtureId, CancellationToken cancellationToken)
        {
            var result = await fixtureFeatureService.PublishFixture(fixtureId, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UnPublishFixture([FromServices] IFixtureFeatureService fixtureFeatureService, [FromQuery] int fixtureId, CancellationToken cancellationToken)
        {
            var result = await fixtureFeatureService.UnPublishFixture(fixtureId, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreateTip([FromServices] IFixtureFeatureService fixtureFeatureService, [FromBody] TipRequestDto tipRequestDto, CancellationToken cancellationToken)
        {
            var result = await fixtureFeatureService.CreateTips(tipRequestDto, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreatePreview([FromServices] IFixtureFeatureService fixtureFeatureService, [FromBody] PreviewRequestDto previewRequestDto, CancellationToken cancellationToken)
        {
            var result = await fixtureFeatureService.CreatePreview(previewRequestDto, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
