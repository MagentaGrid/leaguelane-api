using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Feature
{
    public interface ILeagueFeatureService
    {
        Task<PaginationBaseResponse> GetAllLeagues(int page, int pageSize, string? search, string status, CancellationToken cancellationToken);
        Task<BaseResponse> UpdateLeague(UpdateLeagueRequestDto leagueRequestDto, CancellationToken cancellationToken);
        Task<BaseResponse> DisableLeague(int leagueId, CancellationToken cancellationToken);
        Task<BaseResponse> EnableLeague(int leagueId, CancellationToken cancellationToken);
    }
}
