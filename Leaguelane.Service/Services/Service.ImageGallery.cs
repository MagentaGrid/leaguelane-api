using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class ImageGalleryService : IImageGalleryService
    {
        private readonly IImageGalleryRepository _imageGalleryRepository;
        public ImageGalleryService(IImageGalleryRepository imageGalleryRepository)
        {
            _imageGalleryRepository = imageGalleryRepository;
        }
        public async Task<ImageGallery> SaveImageUrl(string imageUrl, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _imageGalleryRepository.SaveImageUrl(imageUrl, cancellationToken);
        }
        public async Task<List<ImageGallery>> GetAllImages(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _imageGalleryRepository.GetAllImages(cancellationToken);
        }
        public async Task<ImageGallery> GetImageById(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _imageGalleryRepository.GetImageById(id, cancellationToken);
        }
        public async Task<ImageGallery> UpdateImage(ImageGallery image, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _imageGalleryRepository.UpdateImage(image, cancellationToken);
        }
    }
}
