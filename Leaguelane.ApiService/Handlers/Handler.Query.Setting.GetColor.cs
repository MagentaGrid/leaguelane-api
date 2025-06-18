using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using Leaguelane.Constants.Enums;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetColorSettingsQuery() : IRequest<SettingsResponse>;

    public class GetColorSettingsQueryHandler : IRequestHandler<GetColorSettingsQuery, SettingsResponse>
    {
        private readonly ISettingsService _settingsService;

        public GetColorSettingsQueryHandler(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task<SettingsResponse> Handle(GetColorSettingsQuery request, CancellationToken cancellationToken)
        {
            var settings = await _settingsService.GetSettingsByNameAsync(SiteSettings.Color, cancellationToken);
            var colorSetting=new SettingsDto
            {
                SettingsId = settings.SettingsId,
                Name = Enum.Parse<SiteSettings>(settings.Name),
                Value = settings.Value
            };

            return new SettingsResponse(true, null, colorSetting);
        }
    }
}
