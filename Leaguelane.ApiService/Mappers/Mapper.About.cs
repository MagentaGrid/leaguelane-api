using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.Api.Mappers
{
    public static class AboutMapper
    {
        public static About MapAboutDtoToAbout(AboutDto aboutDto)
        {
            return new About
            {
                HeroImageUrl = aboutDto.HeroImageUrl,
                Active = true,
                Created = DateTime.UtcNow,
                MainContent = aboutDto.MainContent,
                Title = aboutDto.Title,
                Subtitle = aboutDto.Subtitle,
            };
        }

        public static AboutResponseDto MapAboutToAboutResponseDto(About about)
        {
            return new AboutResponseDto
            {
                HeroImageUrl = about.HeroImageUrl,
                MainContent = about.MainContent,
                Title = about.Title,
                Subtitle = about.Subtitle,
                Id = about.Id
            };
        }
    }
}
