using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record GetAllMealsQuery() : IRequest<MealListResponse>;
    public class GetAllMealsQueryHandler : IRequestHandler<GetAllMealsQuery, MealListResponse>
    {
        private readonly IMealService _mealService;
        public GetAllMealsQueryHandler(IMealService mealService)
        {
            _mealService = mealService;
        }
        public async Task<MealListResponse> Handle(GetAllMealsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mealService.GetAllMealsAsync();
                return new MealListResponse
                {
                    IsSuccess = true,
                    ErrorMessage = "Meals fetched successfully",
                    Meals = MealMapper.MapMealsToResponseDto(result)
                };
            }
            catch (Exception ex)
            {
                return new MealListResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
