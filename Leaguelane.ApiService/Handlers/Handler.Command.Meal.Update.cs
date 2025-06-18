using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record UpdateMealCommand(int Id, MealRequestDto mealRequestDto): IRequest<MealResponse>;
    public class UpdateMealCommandHandler: IRequestHandler<UpdateMealCommand, MealResponse>
    {
        private readonly IMealService _mealService;
        public UpdateMealCommandHandler(IMealService mealService)
        {
            _mealService = mealService;
        }
        public async Task<MealResponse> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var meal = await _mealService.GetMealByIdAsync(request.Id);
                if (meal == null)
                {
                    return new MealResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Meal not found",
                        Meal = null
                    };
                }

                meal.Name = request.mealRequestDto.Name;
                meal.Price = request.mealRequestDto.Price;
                meal.MealType = request.mealRequestDto.MealType;
                meal.Updated = DateTime.UtcNow;
                var result = await _mealService.UpdateMealAsync(meal);
                return new MealResponse
                {
                    IsSuccess = true,
                    ErrorMessage = "Meal updated successfully",
                    Meal = MealMapper.MapMealToResponseDto(result)
                };
            }
            catch (Exception ex)
            {
                return new MealResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                    Meal = null
                };
            }
        }
    }
}
