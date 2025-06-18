using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class ImageGalleryRepository: IImageGalleryRepository
    {
        private readonly LeaguelaneDbContext _context;
        public ImageGalleryRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<ImageGallery> SaveImageUrl(string imageUrl, CancellationToken cancellationToken = default(CancellationToken))
        {
            var image = new ImageGallery
            {
                ImageUrl = imageUrl,
                Active = true,
                Created = DateTime.UtcNow,
            };
            _context.ImageGalleries.Add(image);
            await _context.SaveChangesAsync(cancellationToken);
            return image;
        }

        public async Task<List<ImageGallery>> GetAllImages(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.ImageGalleries.Where(x => x.Active == true).ToListAsync(cancellationToken);
        }

        public async Task<ImageGallery> GetImageById(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.ImageGalleries.Where(x => x.Active == true && x.ImageId == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ImageGallery> UpdateImage(ImageGallery image, CancellationToken cancellationToken = default(CancellationToken))
        {
            _context.ImageGalleries.Update(image);
            await _context.SaveChangesAsync(cancellationToken);
            return image;
        }
    }
}
