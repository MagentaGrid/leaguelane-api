using Leaguelane.Api.Handlers;
using Leaguelane.ApiService.Feature;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class ArticleEndpoint
    {
        public static RouteGroupBuilder AddArticleRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetArticles).WithName("articles")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapGet("{articleId:int}", GetArticleById).WithName("article-by-id")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPost("", CreateArticle).WithName("article-create")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("", UpdateArticle).WithName("article-update")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapDelete("", DeleteArticle).WithName("article-delete")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPatch("publish", PublishArticle).WithName("article-publish")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        public static async Task<IResult> GetArticles([FromServices]IArticleFeatureService articleFeatureService, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await articleFeatureService.GetAllArticles(page, pageSize, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetArticleById([FromServices] IArticleFeatureService articleFeatureService, int articleId, CancellationToken cancellationToken)
        {
            var result = await articleFeatureService.GetArticleById(articleId, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreateArticle([FromServices] IArticleFeatureService articleFeatureService, [FromBody] ArticleRequestDto articleRequestDto, CancellationToken cancellationToken)
        {
            var result = await articleFeatureService.CreateArticle(articleRequestDto, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateArticle([FromServices] IArticleFeatureService articleFeatureService, [FromBody] ArticleUpdateRequestDto articleRequestDto, CancellationToken cancellationToken)
        {
            var result = await articleFeatureService.UpdateArticle(articleRequestDto, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> DeleteArticle([FromServices] IArticleFeatureService articleFeatureService, [FromQuery] int articleId, CancellationToken cancellationToken)
        {
            var result = await articleFeatureService.DeleteArticle(articleId, cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> PublishArticle([FromServices] IArticleFeatureService articleFeatureService, [FromQuery] int articleId, CancellationToken cancellationToken)
        {
            var result = await articleFeatureService.PublishArticle(articleId, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
