using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record CreateSportCommand(SportDto sportDto): IRequest<SportResponse>;
    public class CreateSportCommandHandler: IRequestHandler<CreateSportCommand, SportResponse>
    {
        private readonly ISportService _sportService;

        public CreateSportCommandHandler(ISportService sportService)
        {
            _sportService = sportService;
        }

        public async Task<SportResponse> Handle(CreateSportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _sportService.AddSport(SportMapper.MapToEntity(request.sportDto), cancellationToken);

                return new SportResponse
                {
                    IsSuccess = true,
                    ErrorMessage = null,
                    Sport = SportMapper.MapToDto(result)
                };
            }
            catch (Exception ex)
            {
                return new SportResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    Sport = null
                };
            }
        }
    }
}
