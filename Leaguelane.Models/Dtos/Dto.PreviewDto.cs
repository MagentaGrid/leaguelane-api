namespace Leaguelane.Models.Dtos
{
    public class PreviewDto
    {
    }

    public class PreviewRequestDto
    {
        public int FixtureId { get; set; }
        public string Headline { get; set; } = string.Empty;
        //public string Url { get; set; } = string.Empty;
        public string ShortIntro { get; set; } = string.Empty;
        public string FullAnalysis { get; set; } = string.Empty;
    }
}
