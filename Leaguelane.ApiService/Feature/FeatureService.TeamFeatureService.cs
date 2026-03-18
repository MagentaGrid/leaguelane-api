using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;

namespace Leaguelane.ApiService.Feature
{
    public class TeamFeatureService: ITeamFeatureService
    {
        private readonly ITeamService _teamService;

        public TeamFeatureService(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<PaginationBaseResponse> GetAllTeams(int page, int pageSize, string? search, string status, CancellationToken cancellationToken)
        {
            var (totalCount, data) = await _teamService.GetAllTeams(page, pageSize, search, status, cancellationToken);

            return new PaginationBaseResponse(true, "Teams fetched successfully", data.Select(TeamMapper.MapToDto).ToList(), page, pageSize, totalCount, (int)Math.Ceiling((double)totalCount / pageSize));
        }

        public async Task<BaseResponse> UpdateTeam(TeamUpdateDto teamRequestDto, CancellationToken cancellationToken)
        {
            var data = await _teamService.UpdateTeamAsync(teamRequestDto, cancellationToken);

            return new BaseResponse(true, "Team updated successfully", data);
        }

        public async Task<BaseResponse> DisableTeam(int teamId, CancellationToken cancellationToken)
        {
            var data = await _teamService.DisableTeamAsync(teamId, cancellationToken);
            return new BaseResponse(true, "Team disabled successfully", data);
        }

        public async Task<BaseResponse> EnableTeam(int teamId, CancellationToken cancellationToken)
        {
            var data = await _teamService.EnableTeamAsync(teamId, cancellationToken);
            return new BaseResponse(true, "Team enabled successfully", data);
        }
    }
}
