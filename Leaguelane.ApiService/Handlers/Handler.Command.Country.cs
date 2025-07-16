using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record CountrySchedulerCommand(): IRequest<SchedulerResponseDto>;
    public class CountrySchedulerCommandHandler: IRequestHandler<CountrySchedulerCommand, SchedulerResponseDto>
    {
        private readonly ICountryService _countryService;
        public CountrySchedulerCommandHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }
        public async Task<SchedulerResponseDto> Handle(CountrySchedulerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _countryService.GetAllCountriesAsync(cancellationToken);
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
