using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record GetAllSportsQuery(): IRequest<SportsResponse>;
    public class GetAllSportsQueryHandler: IRequestHandler<GetAllSportsQuery,SportsResponse>
    {
        private readonly ISportService _sportService;
        public GetAllSportsQueryHandler(ISportService sportService)
        {
            _sportService = sportService;
        }

        public async Task<SportsResponse> Handle(GetAllSportsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _sportService.GetAllSports(cancellationToken);

                return new SportsResponse
                {
                    IsSuccess = true,
                    ErrorMessage = null,
                    Sports = result.Select(x => SportMapper.MapToDto(x)).ToList()
                };
            }
            catch (Exception ex)
            {
                return new SportsResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    Sports = null
                };
            }
        }
    }
}
