using Leaguelane.ApiService.Feature;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.ApiService.Endpoints
{
    public static class FileEndpoint
    {
        public static RouteGroupBuilder AddFileRoutes(this RouteGroupBuilder group)
        {
            group.MapPost("", UploadFile).WithName("file-upload")
                .DisableAntiforgery()
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        public static async Task<IResult> UploadFile([FromServices] IFileFeatureService fileFeatureService, [FromForm] FileUploadRequest fileUploadRequest, CancellationToken cancellationToken)
        {
            var result = await fileFeatureService.UploadFile(fileUploadRequest.File, fileUploadRequest.Module, cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
