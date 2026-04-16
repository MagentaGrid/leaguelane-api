using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Feature
{
    public interface IBookmakerFeatureService
    {
        Task<PaginationBaseResponse> GetAllBookmakers(int page = 1, int pageSize = 10, string search = "", CancellationToken cancellationToken = default);
        Task<BaseResponse> EnableBookmaker(int bookmakerId, CancellationToken cancellationToken);
        Task<BaseResponse> DisableBookmaker(int bookmakerId, CancellationToken cancellationToken);
    }
}
