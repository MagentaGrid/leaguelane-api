using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record LeagueSchedulerCommand(): IRequest<SchedulerResponseDto>;
    public class LeagueSchedulerCommandHandler: IRequestHandler<LeagueSchedulerCommand, SchedulerResponseDto>
    {
        private readonly ILeagueService _leagueService;
        public LeagueSchedulerCommandHandler(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }
        public async Task<SchedulerResponseDto> Handle(LeagueSchedulerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _leagueService.GetAllLeaguesAsync(cancellationToken);
                return new SchedulerResponseDto
                {
                    IsSuccess = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new SchedulerResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
