using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record CreateImageGalleryCommand(IFormFile image): IRequest<ImageGalleryResponse>;
    public class CreateImageGalleryCommandHandler: IRequestHandler<CreateImageGalleryCommand, ImageGalleryResponse>
    {
        private readonly IImageGalleryService _imageGalleryService;
        private readonly IBlobService _blobService;
        public CreateImageGalleryCommandHandler(IImageGalleryService imageGalleryService, IBlobService blobService)
        {
            _imageGalleryService = imageGalleryService;
            _blobService = blobService;
        }
        public async Task<ImageGalleryResponse> Handle(CreateImageGalleryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var imageUrl = await _blobService.UploadImageAsync(request.image);
                var imageGallery = await _imageGalleryService.SaveImageUrl(imageUrl, cancellationToken);
                return new ImageGalleryResponse
                {
                    IsSuccess = true,
                    ErrorMessage = "Image uploaded successfully",
                    ImageGallery = ImageGalleryMapper.MapImageGalleryToResponseDto(imageGallery)
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
