using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record UpdateSettingsCommand(SettingsDto Settings, int SettingId) : IRequest<SettingsResponse>;
    public class UpdateSettingsCommandHandler : IRequestHandler<UpdateSettingsCommand, SettingsResponse>
    {
        private readonly ISettingsService _settingsService;

        public UpdateSettingsCommandHandler(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task<SettingsResponse> Handle(UpdateSettingsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingSettings = await _settingsService.GetSettingsByIdAsync(request.SettingId, cancellationToken);
                if (existingSettings != null)
                {
                    existingSettings.Name = request.Settings.Name.ToString();
                    existingSettings.Value = request.Settings.Value;
                    existingSettings.Updated = DateTime.UtcNow;

                    var result = await _settingsService.UpdateSettings(existingSettings, cancellationToken);

                    var updatedSettings = new SettingsDto
                    {
                        SettingsId = result.SettingsId,
                        Name = Enum.Parse<SiteSettings>(result.Name),
                        Value = result.Value,
                    };

                    return new SettingsResponse(true, "Updated successfully", updatedSettings);
                }

                return new SettingsResponse(false, "Settings not found", null);
            }
            catch (Exception ex)
            {
                return new SettingsResponse(false, ex.Message, null);
            }
        }
    }
}

