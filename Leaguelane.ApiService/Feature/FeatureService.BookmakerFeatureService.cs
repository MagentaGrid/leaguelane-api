using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;

namespace Leaguelane.ApiService.Feature
{
    public class BookmakerFeatureService: IBookmakerFeatureService
    {
        private readonly IBookmakerService _bookmakerService;
        public BookmakerFeatureService(IBookmakerService bookmakerService)
        {
            _bookmakerService = bookmakerService;
        }

        public async Task<PaginationBaseResponse> GetAllBookmakers(int page = 1, int pageSize = 10, string search = "", CancellationToken cancellationToken = default)
        {
            var (totalCount, bookmakers) = await _bookmakerService.GetAllBookmakersAsync(page, pageSize, search, cancellationToken);

            if (bookmakers == null)
            {
                return new PaginationBaseResponse(true, "Bookmakers not found", null);
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            return new PaginationBaseResponse(true, "Bookmakers fetched successfully", bookmakers.Select(BookmakerMapper.MapToDto).ToList(), page, pageSize, totalCount, totalPages);
        }

        public async Task<BaseResponse> EnableBookmaker(int bookmakerId, CancellationToken cancellationToken)
        {
            await _bookmakerService.EnableBookmakerAsync(bookmakerId, cancellationToken);

            return new BaseResponse(true, "Bookmaker enabled successfully", true);
        }

        public async Task<BaseResponse> DisableBookmaker(int bookmakerId, CancellationToken cancellationToken)
        {
            await _bookmakerService.DisableBookmakerAsync(bookmakerId, cancellationToken);

            return new BaseResponse(true, "Bookmaker disabled successfully", true);
        }
    }
}
