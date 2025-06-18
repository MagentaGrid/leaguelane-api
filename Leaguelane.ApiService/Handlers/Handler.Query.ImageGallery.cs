using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetAllImageGalleryQuery() : IRequest<ImageGalleryListResponse>;
    public class GetAllImageGalleryQueryHandler: IRequestHandler<GetAllImageGalleryQuery, ImageGalleryListResponse>
    {
        private readonly IImageGalleryService _imageGalleryService;
        public GetAllImageGalleryQueryHandler(IImageGalleryService imageGalleryService)
        {
            _imageGalleryService = imageGalleryService;
        }
        public async Task<ImageGalleryListResponse> Handle(GetAllImageGalleryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _imageGalleryService.GetAllImages();
                return new ImageGalleryListResponse
                {
                    IsSuccess = true,
                    ErrorMessage = "Image gallery fetched successfully",
                    ImageGalleries = ImageGalleryMapper.MapImageGalleryToResponseListDto(result)
                };
            }
            catch (Exception ex)
            {
                return new ImageGalleryListResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
