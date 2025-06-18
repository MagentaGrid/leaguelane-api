using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetAboutQuery() : IRequest<AboutsResponse>;
    public class GetAboutQueryHandler : IRequestHandler<GetAboutQuery, AboutsResponse>
    {
        private readonly IAboutService _aboutService;
        public GetAboutQueryHandler(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public async Task<AboutsResponse> Handle(GetAboutQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var about = await _aboutService.GetAllAbouts(cancellationToken);

                return new AboutsResponse
                {
                    Abouts = about.Select(x=> AboutMapper.MapAboutToAboutResponseDto(x)).ToList(),
                    ErrorMessage = null,
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new AboutsResponse
                {
                    Abouts = null,
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
        }
    }
}
