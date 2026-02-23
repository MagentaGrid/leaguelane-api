using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    public class FixturePreview : Entity
    {
        [Key]
        public int FixturePreviewId { get; set; }
        public int FixtureId { get; set; }
        public string Headline { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ShortIntro { get; set; } = string.Empty;
        public string FullAnalysis { get; set; } = string.Empty;

        [ForeignKey("FixtureId")]
        public Fixture Fixture { get; set; }
    }
}
