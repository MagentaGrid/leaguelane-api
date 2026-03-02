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
            group.MapGet("", GetFixtures).WithName("fixtures").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("{id:int}", GetFixtureById).WithName("fixture-by-id").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("publish", PublishFixture).WithName("fixture-publish").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("unpublish", UnPublishFixture).WithName("fixture-unpublish").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPost("tip", CreateTip).WithName("fixture-tip-create").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPost("preview", CreatePreview).WithName("fixture-preview-create").Produces<object>(200)
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            //group.MapPut("{id:int}/rank", SetRank).WithName("fixture-set-rank");
            //group.MapGet("latest", GetLatestFixtures).WithName("fixtures-latest");
            return group;
        }

        /// <summary>
        /// Get fixtures (admin).
        /// Returns paginated fixtures.
        /// </summary>
        /// <param name="fixtureFeatureService"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetFixtures([FromServices] IFixtureFeatureService fixtureFeatureService, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await fixtureFeatureService.GetFixtures(page, pageSize, cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Get fixture by id (admin).
        /// Returns fixture details by id.
        /// </summary>
        /// <param name="fixtureFeatureService"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> GetFixtureById([FromServices] IFixtureFeatureService fixtureFeatureService, int id, CancellationToken cancellationToken)
        {
            var result = await fixtureFeatureService.GetFixtureDetailsById(id, cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Publish fixture (admin).
        /// Publishes the fixture with specified id.
        /// </summary>
        /// <param name="fixtureFeatureService"></param>
        /// <param name="fixtureId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> PublishFixture([FromServices] IFixtureFeatureService fixtureFeatureService, [FromQuery] int fixtureId, CancellationToken cancellationToken)
        {
            var result = await fixtureFeatureService.PublishFixture(fixtureId, cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Unpublish fixture (admin).
        /// Unpublishes the fixture with specified id.
        /// </summary>
        /// <param name="fixtureFeatureService"></param>
        /// <param name="fixtureId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> UnPublishFixture([FromServices] IFixtureFeatureService fixtureFeatureService, [FromQuery] int fixtureId, CancellationToken cancellationToken)
        {
            var result = await fixtureFeatureService.UnPublishFixture(fixtureId, cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Create tip (admin).
        /// Adds a tip for a fixture.
        /// </summary>
        /// <param name="fixtureFeatureService"></param>
        /// <param name="tipRequestDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> CreateTip([FromServices] IFixtureFeatureService fixtureFeatureService, [FromBody] TipRequestDto tipRequestDto, CancellationToken cancellationToken)
        {
            var result = await fixtureFeatureService.CreateTips(tipRequestDto, cancellationToken);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// Create preview (admin).
        /// Adds a preview for a fixture.
        /// </summary>
        /// <param name="fixtureFeatureService"></param>
        /// <param name="previewRequestDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<IResult> CreatePreview([FromServices] IFixtureFeatureService fixtureFeatureService, [FromBody] PreviewRequestDto previewRequestDto, CancellationToken cancellationToken)
        {
            var result = await fixtureFeatureService.CreatePreview(previewRequestDto, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
