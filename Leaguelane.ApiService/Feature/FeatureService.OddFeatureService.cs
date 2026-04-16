using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;

namespace Leaguelane.ApiService.Feature
{
    public class OddFeatureService: IOddFeatureService
    {
        private readonly IOddsService _oddsService;
        private readonly IBookmakerService _bookmakerService;
        private readonly IBetService _betService;
        public OddFeatureService(IOddsService oddsService, IBookmakerService bookmakerService, IBetService betService)
        {
            _oddsService = oddsService;
            _bookmakerService = bookmakerService;
            _betService = betService;
        }

        public async Task<BaseResponse> GetAllOddsForBetAndBookmaker(int betId, int bookmakerId, int fixtureId, CancellationToken cancellationToken)
        {
            var odds = await _oddsService.GetOddsByBetAndBookmakerIdAsync(betId, bookmakerId, fixtureId, cancellationToken);

            if (odds == null)
            {
                return new BaseResponse(false, "Odds not found", null);
            }

            return new BaseResponse(true, "Odds fetched successfully", OddMapper.MapToOddResponseDtos(odds));
        }

        public async Task<BaseResponse> GetAllBookmakers(CancellationToken cancellationToken)
        {
            var bookmakers = await _bookmakerService.GetAllActiveBookmakersAsync(cancellationToken);

            if (bookmakers == null)
            {
                return new BaseResponse(false, "Bookmakers not found", null);
            }

            return new BaseResponse(true, "Bookmakers fetched successfully", bookmakers.Select(BookmakerMapper.MapToDto).ToList());
        }

        public async Task<BaseResponse> GetAllBets(CancellationToken cancellationToken)
        {
            var bets = await _betService.GetAllBets(cancellationToken);

            if (bets == null)
            {
                return new BaseResponse(false, "Bets not found", null);
            }

            return new BaseResponse(true, "Bets fetched successfully", bets.Select(BetMapper.MapToDto).ToList());
        }
    }
}
