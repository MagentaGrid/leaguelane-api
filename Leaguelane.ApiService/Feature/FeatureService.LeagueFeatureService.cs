using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;

namespace Leaguelane.ApiService.Feature
{
    public class LeagueFeatureService: ILeagueFeatureService
    {
        private readonly ILeagueService _leagueService;
        public LeagueFeatureService(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        public async Task<PaginationBaseResponse> GetAllLeagues(int page, int pageSize, string? search, string status, CancellationToken cancellationToken)
        {
            var (totalCount, data) = await _leagueService.GetAllLeagues(page, pageSize, search, status, cancellationToken);

            return new PaginationBaseResponse(true, "Leagues fetched successfully", data.Select(LeagueMapper.MapToDto).ToList(), page, pageSize, totalCount, (int)Math.Ceiling((double)totalCount / pageSize));
        }

        public async Task<BaseResponse> UpdateLeague(UpdateLeagueRequestDto leagueRequestDto, CancellationToken cancellationToken)
        {
            var data = await _leagueService.UpdateLeagueAsync(leagueRequestDto, cancellationToken);

            return new BaseResponse(true, "League updated successfully", data);
        }

        public async Task<BaseResponse> DisableLeague(int leagueId, CancellationToken cancellationToken)
        {
            var data = await _leagueService.DisableLeagueAsync(leagueId, cancellationToken);

            return new BaseResponse(true, "League disabled successfully", data);
        }

        public async Task<BaseResponse> EnableLeague(int leagueId, CancellationToken cancellationToken)
        {
            var data = await _leagueService.EnableLeagueAsync(leagueId, cancellationToken);

            return new BaseResponse(true, "League disabled successfully", data);
        }
    }
}
