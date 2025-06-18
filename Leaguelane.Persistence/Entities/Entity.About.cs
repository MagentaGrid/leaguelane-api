using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    public class About : Entity
    {
        public int Id { get; set; }
        public string Title { get; set; }            // e.g., "About Our Company"
        public string Subtitle { get; set; }         // e.g., "Committed to Innovation"
        public string HeroImageUrl { get; set; }     // e.g., "/images/about-hero.jpg"
        public string MainContent { get; set; }      // Rich text or HTML describing your company
    }
}
