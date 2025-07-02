using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record GetSportByIdQuery(int Id) : IRequest<SportResponse>;
    public class GetSportByIdQueryHandler: IRequestHandler<GetSportByIdQuery, SportResponse>
    {
        private readonly ISportService _sportService;
        public GetSportByIdQueryHandler(ISportService sportService)
        {
            _sportService = sportService;
        }

        public async Task<SportResponse> Handle(GetSportByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _sportService.GetSport(request.Id, cancellationToken);
                if (result == null)
                {
                    return new SportResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Sport not found",
                        Sport = null
                    };
                }

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
