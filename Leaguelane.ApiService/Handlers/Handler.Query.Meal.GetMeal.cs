using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record GetMealByIdQuery(int id) : IRequest<MealResponse>;
    public class GetMealByIdQueryHandler: IRequestHandler<GetMealByIdQuery, MealResponse>
    {
        private readonly IMealService _mealService;
        public GetMealByIdQueryHandler(IMealService mealService)
        {
            _mealService = mealService;
        }
        public async Task<MealResponse> Handle(GetMealByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mealService.GetMealByIdAsync(request.id);
                if (result == null)
                {
                    return new MealResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Meal not found"
                    };
                }

                return new MealResponse
                {
                    IsSuccess = true,
                    ErrorMessage = "Meal details fetched successfully",
                    Meal = MealMapper.MapMealToResponseDto(result)
                };
            }
            catch (Exception ex)
            {
                return new MealResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
