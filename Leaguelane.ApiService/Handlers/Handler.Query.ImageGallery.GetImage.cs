using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetImageGalleryByIdQuery(int id) : IRequest<ImageGalleryResponse>;
    public class GetImageGalleryByIdQueryHandler: IRequestHandler<GetImageGalleryByIdQuery, ImageGalleryResponse>
    {
        private readonly IImageGalleryService _imageGalleryService;
        
        public GetImageGalleryByIdQueryHandler(IImageGalleryService imageGalleryService)
        {
            _imageGalleryService = imageGalleryService;
        }
        public async Task<ImageGalleryResponse> Handle(GetImageGalleryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _imageGalleryService.GetImageById(request.id, cancellationToken);
                if (result == null)
                {
                    return new ImageGalleryResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Image not found",
                        ImageGallery = null
                    };
                }

                return new ImageGalleryResponse
                {
                    IsSuccess = true,
                    ErrorMessage = "Image fetched successfully",
                    ImageGallery = ImageGalleryMapper.MapImageGalleryToResponseDto(result)
                };
            }
            catch (Exception ex)
            {
                return new ImageGalleryResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    ImageGallery = null
                };
            }
        }
    }
}
