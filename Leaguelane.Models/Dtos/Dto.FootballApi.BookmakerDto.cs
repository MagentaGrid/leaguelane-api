namespace Leaguelane.Models.Dtos
{
    public class BookmakerDto
    {
        public int Id { get; set; }
        public int? ApiBookMakerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? AffiliateLink { get; set; }
        public string? BookieLogo { get; set; }
        public bool? Active { get; set; }
    }
}
