using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Feature
{
    public interface IFixtureFeatureService
    {
        Task<PaginationBaseResponse> GetFixtures(int page, int pageSize, CancellationToken cancellationToken);
        Task<BaseResponse> PublishFixture(int fixtureId, CancellationToken cancellationToken);
        Task<BaseResponse> UnPublishFixture(int fixtureId, CancellationToken cancellationToken);
        Task<BaseResponse> CreateTips(TipRequestDto tipRequestDto, CancellationToken cancellationToken);
        Task<BaseResponse> CreatePreview(PreviewRequestDto previewRequestDto, CancellationToken cancellationToken);
        Task<BaseResponse> GetFixtureDetailsById(int fixtureId, CancellationToken cancellationToken);
        Task<BaseResponse> UpdateTip(TipUpdateRequestDto tipRequestDto, CancellationToken cancellationToken);
        Task<BaseResponse> DeleleteTip(int tipId, CancellationToken cancellationToken);
        Task<BaseResponse> GetFeaturedPredictions(int count, CancellationToken cancellationToken);
        Task<BaseResponse> GetPredictions(string league, int page, int pageSize, CancellationToken cancellationToken);
        Task<PaginationBaseResponse> GetFixturesByLeague(int page, int pageSize, int leagueId, CancellationToken cancellationToken);
    }
}
