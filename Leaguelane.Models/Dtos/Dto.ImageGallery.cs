using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class ImageGalleryDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool Active { get; set; }
    }

    public class ImageGalleryListResponse : Response
    {
        public List<ImageGalleryResponseDto> ImageGalleries { get; set; }
    }

    public class ImageGalleryResponseDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ImageGalleryResponse : Response
    {
        public ImageGalleryResponseDto? ImageGallery { get; set; }
    }
}
