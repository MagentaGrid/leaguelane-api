using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class AboutDto
    {
        public int Id { get; set; }
        public string Title { get; set; }            
        public string Subtitle { get; set; }         
        public string HeroImageUrl { get; set; }   
        public string MainContent { get; set; }
        public bool Active { get; set; }
    }

    public class AboutResponse : Response
    {
        public AboutResponseDto About { get; set; }
    }

    public class AboutResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string HeroImageUrl { get; set; }
        public string MainContent { get; set; }
    }

    public class AboutsResponse : Response
    {
        public List<AboutResponseDto> Abouts { get; set; }
    }
}
