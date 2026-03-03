using System.ComponentModel.DataAnnotations;

namespace Leaguelane.Persistence.Entities
{
    public class Article : Entity
    {
        [Key]
        public int ArticleId { get; set; }
        public string? Headline { get; set; }
        public string? FullAnalysis { get; set; }
        public string? Link { get; set; }
        public string? ShortIntro { get; set; }
        public bool IsPublished { get; set; } = false;
        public DateTime? PublishDate { get; set; }
    }
}
