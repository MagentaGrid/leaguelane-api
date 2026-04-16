namespace Leaguelane.Models.Dtos
{
    public class BookmakerResponseDto
    {
        public int BookmakerId { get; set; }
        public string Name { get; set; }
        public int ApiBookmakerId { get; set; }
        public bool Active { get; set; } = false;
    }
}
