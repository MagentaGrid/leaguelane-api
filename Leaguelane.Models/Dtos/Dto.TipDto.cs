namespace Leaguelane.Models.Dtos
{
    public class TipDto
    {
    }

    public class TipRequestDto
    {
        public int FixtureId { get; set; }
        public string Title { get; set; }
        public string? Reasoning { get; set; }
        public int BookmakerId { get; set; }
        public int OddsId { get; set; }
        public int BetId { get; set; }
        public bool IsSaved { get; set; } = true;
        public bool IsVisible { get; set; } = false;
    }

    public class TipUpdateRequestDto: TipRequestDto
    {
        public int FixtureTipId { get; set; }
    }
}
