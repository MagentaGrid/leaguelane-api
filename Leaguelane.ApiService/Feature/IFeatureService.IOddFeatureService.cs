using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Feature
{
    public interface IOddFeatureService
    {
        Task<BaseResponse> GetAllOddsForBetAndBookmaker(int betId, int bookmakerId, int fixtureId, CancellationToken cancellationToken);
        Task<BaseResponse> GetAllBookmakers(CancellationToken cancellationToken);
        Task<BaseResponse> GetAllBets(CancellationToken cancellationToken);
    }
}
