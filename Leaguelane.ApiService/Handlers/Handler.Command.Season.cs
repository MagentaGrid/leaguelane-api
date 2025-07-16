using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record SeasonScheduerCommand(): IRequest<SchedulerResponseDto>;
    public class SeasonScheduerCommandHandler : IRequestHandler<SeasonScheduerCommand, SchedulerResponseDto>
    {
        private readonly ISeasonService _seasonService;

        public SeasonScheduerCommandHandler(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }
        public async Task<SchedulerResponseDto> Handle(SeasonScheduerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _seasonService.GetAllSeasons(cancellationToken);
                return new SchedulerResponseDto
                {
                    IsSuccess = true,
                    ErrorMessage = null,
                };
            }
            catch (Exception ex)
            {
                return new SchedulerResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
