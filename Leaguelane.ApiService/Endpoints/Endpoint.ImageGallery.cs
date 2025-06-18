using Leaguelane.Api.Handlers;
using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Leaguelane.Api.Endpoints
{
    public static class ImageGalleryEndpoint
    {
        public static RouteGroupBuilder AddImageGalleryRoutes(this RouteGroupBuilder group)
        {
            group.MapGet("", GetImageAllGallery).WithName("get-image-gallery");

            group.MapGet("{id}", GetImageGallery).WithName("get-image-gallery-by-id")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPost("", CreateImageGallery).DisableAntiforgery().WithName("create-image-gallery")
                .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            group.MapPut("{id}", UpdateImageGallery).WithName("update-image-gallery")
                 .RequireAuthorization(policy => policy.RequireRole(UserRole.Admin.ToString(), UserRole.Employee.ToString()));

            return group;
        }

        public static async Task<IResult> GetImageAllGallery(ISender sender, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetAllImageGalleryQuery(), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> GetImageGallery(ISender sender, int id, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new GetImageGalleryByIdQuery(id), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> CreateImageGallery(ISender sender, IFormFile image, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new CreateImageGalleryCommand(image), cancellationToken);
            return TypedResults.Ok(result);
        }

        public static async Task<IResult> UpdateImageGallery(ISender sender, int id, [FromBody] ImageGalleryDto imageGallery, CancellationToken cancellationToken)
        {
            var result = await sender.Send(new UpdateImageGalleryCommand(imageGallery, id), cancellationToken);
            return TypedResults.Ok(result);
        }
    }
}
