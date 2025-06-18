using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record UpdateAboutCommand(AboutDto AboutDto) : IRequest<AboutResponse>;
    public class UpdateAboutCommandHandler: IRequestHandler<UpdateAboutCommand, AboutResponse>
    {
        private readonly IAboutService _aboutService;
        public UpdateAboutCommandHandler(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public async Task<AboutResponse> Handle(UpdateAboutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var about = await _aboutService.GetAbout(request.AboutDto.Id, cancellationToken);
                if (about == null)
                {
                    return new AboutResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "About not found",
                        About = null
                    };
                }

                about.Subtitle = request.AboutDto.Subtitle;
                about.HeroImageUrl = request.AboutDto.HeroImageUrl;
                about.Updated = DateTime.UtcNow;
                about.Title = request.AboutDto.Title;
                about.MainContent = request.AboutDto.MainContent;
                about.Active = request.AboutDto.Active;

                var result = await _aboutService.UpdateAbout(about, cancellationToken);
                return new AboutResponse
                {
                    IsSuccess = true,
                    ErrorMessage = "About updated successfully",
                    About = AboutMapper.MapAboutToAboutResponseDto(result)
                };
            }
            catch (Exception ex)
            {
                return new AboutResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    About = null
                };
            }
        }
    }
}
