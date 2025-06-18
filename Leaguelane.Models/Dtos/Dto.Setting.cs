using Leaguelane.Constants.Enums;

namespace Leaguelane.Models.Dtos
{
    public class SettingsDto
    {
        public int SettingsId { get; set; }
        public SiteSettings Name { get; set; }
        public string Value { get; set; }
    }

    public record SettingsResponse(bool IsSuccess, string? Message, SettingsDto Settings);
}

