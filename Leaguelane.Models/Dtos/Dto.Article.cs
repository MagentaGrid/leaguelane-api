namespace Leaguelane.Models.Dtos
{
    public class ArticleDto
    {
        public string? Headline { get; set; }
        public string? FullAnalysis { get; set; }
        public string? Link { get; set; }
        public string? ShortIntro { get; set; }
        public bool IsPublished { get; set; } = false;
        public DateTime? PublishDate { get; set; }
    }

    public class ArticleRequestDto : ArticleDto
    {
    }

    public class ArticleResponseDto : ArticleDto
    {
        public int ArticleId { get; set; }
    }

    public class ArticleUpdateRequestDto : ArticleRequestDto
    {
        public int ArticleId { get; set; }
    }
}
