using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Repository.Repositories;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record UpdateImageGalleryCommand(ImageGalleryDto imageGallery, int id) : IRequest<ImageGalleryResponse>;
    public class UpdateImageGalleryCommandHandler: IRequestHandler<UpdateImageGalleryCommand, ImageGalleryResponse>
    {
        private readonly IImageGalleryRepository _imageGalleryRepository;
        public UpdateImageGalleryCommandHandler(IImageGalleryRepository imageGalleryRepository)
        {
            _imageGalleryRepository = imageGalleryRepository;
        }

        public async Task<ImageGalleryResponse> Handle(UpdateImageGalleryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var image = await _imageGalleryRepository.GetImageById(request.id, cancellationToken);
                if (image == null)
                {
                    return new ImageGalleryResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Image not found",
                        ImageGallery = null
                    };
                }
                image.ImageUrl = request.imageGallery.ImageUrl;
                image.Active = request.imageGallery.Active;
                image.Updated = DateTime.UtcNow;
                var result = await _imageGalleryRepository.UpdateImage(image, cancellationToken);
                return new ImageGalleryResponse
                {
                    IsSuccess = true,
                    ErrorMessage = "Image updated successfully",
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
