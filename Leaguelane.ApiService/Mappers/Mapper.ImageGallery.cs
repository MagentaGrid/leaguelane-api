using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.Api.Mappers
{
    public static class ImageGalleryMapper
    {
        public static ImageGallery MapImageGalleryDtoToImageGallery(ImageGalleryDto imageGalleryDto)
        {
            return new ImageGallery
            {
                ImageUrl = imageGalleryDto.ImageUrl,
                Active = true,
                Created = DateTime.UtcNow
            };
        }
        
        public static ImageGalleryResponseDto MapImageGalleryToResponseDto(ImageGallery image)
        {
            return new ImageGalleryResponseDto
            {
                Id = image.ImageId,
                ImageUrl = image.ImageUrl,
            };
        }

        public static List<ImageGalleryResponseDto> MapImageGalleryToResponseListDto(List<ImageGallery> imageGallery)
        {
            return imageGallery.Select(image => new ImageGalleryResponseDto
            {
                Id = image.ImageId,
                ImageUrl = image.ImageUrl,
            }).ToList();
        }
    }
}
