using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Mappers
{
    public static class ArticleMapper
    {
        public static ArticleResponseDto MapToDto(Persistence.Entities.Article article)
        {
            return new ArticleResponseDto
            {
                ArticleId = article.ArticleId,
                Headline = article.Headline,
                FullAnalysis = article.FullAnalysis,
                ShortIntro = article.ShortIntro,
                PublishDate = article.PublishDate,
                IsPublished = article.IsPublished
            };
        }

        public static Persistence.Entities.Article MapToEntity(ArticleRequestDto articleRequestDto)
        {
            return new Persistence.Entities.Article
            {
                Headline = articleRequestDto.Headline,
                FullAnalysis = articleRequestDto.FullAnalysis,
                ShortIntro = articleRequestDto.ShortIntro,
                PublishDate = articleRequestDto.PublishDate,
                IsPublished = articleRequestDto.IsPublished,
                Active = true,
                Created = DateTime.UtcNow
            };
        }
    }
}
