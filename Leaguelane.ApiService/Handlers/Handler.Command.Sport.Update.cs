using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record UpdateSportCommand(SportDto sport, int sportId): IRequest<SportResponse>;
    public class UpdateSportCommandHandler: IRequestHandler<UpdateSportCommand, SportResponse>
    {
        private readonly ISportService _sportService;
        public UpdateSportCommandHandler(ISportService sportService)
        {
            _sportService = sportService;
        }
        public async Task<SportResponse> Handle(UpdateSportCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sport = await _sportService.GetSport(request.sportId, cancellationToken);

                if(sport == null)
                {
                    return new SportResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Sport not found",
                        Sport = null
                    };
                }

                sport.Name = request.sport.Name;
                sport.Description = request.sport.Description;
                sport.Active = request.sport.Active;
                sport.ApiHost = request.sport.ApiHost;
                sport.ApiKey = request.sport.ApiKey;
                sport.ApiUrl = request.sport.ApiUrl;
                sport.Updated = DateTime.UtcNow;

                var result = await _sportService.UpdateSport(sport, cancellationToken);

                return new SportResponse
                {
                    IsSuccess = true,
                    Sport = SportMapper.MapToDto(result),
                    ErrorMessage = "Sport updated successfully"
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
