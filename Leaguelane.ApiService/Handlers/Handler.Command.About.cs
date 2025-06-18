using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record CreateAboutCommand(AboutDto About) : IRequest<AboutResponse>;
    public class CreateAboutCommandHandler : IRequestHandler< CreateAboutCommand, AboutResponse>
    {
        private readonly IAboutService _aboutService;
        public CreateAboutCommandHandler(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<AboutResponse> Handle(CreateAboutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _aboutService.CreateAbout(AboutMapper.MapAboutDtoToAbout(request.About), cancellationToken);
                return new AboutResponse
                {
                    IsSuccess = true,
                    ErrorMessage = null,
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
