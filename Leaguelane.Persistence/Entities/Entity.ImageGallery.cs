using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    public class ImageGallery: Entity
    {
        [Key]
        public int ImageId { get; set; } // Primary Key
        public string ImageUrl { get; set; }
    }
}
