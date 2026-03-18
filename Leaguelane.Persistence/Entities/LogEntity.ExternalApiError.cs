namespace Leaguelane.Persistence.Entities
{
    public class ExternalApiError
    {
        public int ExternalApiErrorId { get; set; }
        public string? ErrorMessage { get; set; }
        public string? RawRequest { get; set; }
        public string? RawResponse { get; set; }
        public DateTime DateTime { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
    }
}
