using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record CreateMealCommand(MealRequestDto MealRequestDto) : IRequest<MealResponse>;
    public class CreateMealCommandHandler: IRequestHandler<CreateMealCommand, MealResponse>
    {
        private readonly IMealService _mealService;
        private readonly IBlobService _blobService;
        public CreateMealCommandHandler(IMealService mealService, IBlobService blobService)
        {
            _mealService = mealService;
            _blobService = blobService;
        }
        public async Task<MealResponse> Handle(CreateMealCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var imageUrl = await _blobService.UploadImageAsync(request.MealRequestDto.Image);
                var meal = await _mealService.AddMealAsync(MealMapper.MapRequestDtoToMeal(request.MealRequestDto, imageUrl));
                return new MealResponse
                {
                    IsSuccess = true,
                    ErrorMessage = "Meal added successfully",
                    Meal = MealMapper.MapMealToResponseDto(meal)
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
