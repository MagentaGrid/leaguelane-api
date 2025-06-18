using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IImageGalleryService
    {
        Task<ImageGallery> SaveImageUrl(string imageUrl, CancellationToken cancellationToken = default(CancellationToken));
        Task<List<ImageGallery>> GetAllImages(CancellationToken cancellationToken = default(CancellationToken));
        Task<ImageGallery> GetImageById(int id, CancellationToken cancellationToken = default(CancellationToken));
        Task<ImageGallery> UpdateImage(ImageGallery image, CancellationToken cancellationToken = default(CancellationToken));
    }
}
