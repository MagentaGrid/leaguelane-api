using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.ApiService.Mappers
{
    public static class PreviewMapper
    {
        public static FixturePreview MapToEntity(PreviewRequestDto previewRequestDto)
        {
            return new FixturePreview
            {
                FixtureId = previewRequestDto.FixtureId,
                Active = true,
                FullAnalysis = previewRequestDto.FullAnalysis,
                Headline = previewRequestDto.Headline,
                ShortIntro = previewRequestDto.ShortIntro,
            };
        }
    }
}
