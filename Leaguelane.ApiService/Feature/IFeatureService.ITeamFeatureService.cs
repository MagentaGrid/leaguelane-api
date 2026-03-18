using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Feature
{
    public interface ITeamFeatureService
    {
        Task<PaginationBaseResponse> GetAllTeams(int page, int pageSize, string? search, string status, CancellationToken cancellationToken);
        Task<BaseResponse> UpdateTeam(TeamUpdateDto teamRequestDto, CancellationToken cancellationToken);
        Task<BaseResponse> DisableTeam(int teamId, CancellationToken cancellationToken);
        Task<BaseResponse> EnableTeam(int teamId, CancellationToken cancellationToken);
    }
}
