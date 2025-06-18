using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.ApiService.Mappers
{
    public static class MealMapper
    {
        public static MealResponseDto MapMealToResponseDto(Meal meal)
        {
            return new MealResponseDto
            {
                Id = meal.MealID,
                Name = meal.Name,
                Price = meal.Price,
                ImageUrl = meal.ImageUrl,
                MealType = meal.MealType
            };
        }

        public static List<MealResponseDto> MapMealsToResponseDto(List<Meal> meals)
        {
            return meals.Select(meal => new MealResponseDto
            {
                Id = meal.MealID,
                Name = meal.Name,
                Price = meal.Price,
                ImageUrl = meal.ImageUrl,
                MealType = meal.MealType
            }).ToList();
        }

        public static Meal MapRequestDtoToMeal(MealRequestDto mealRequestDto, string url)
        {
            return new Meal
            {
                Name = mealRequestDto.Name,
                Price = mealRequestDto.Price,
                MealType = mealRequestDto.MealType,
                ImageUrl = url,
                Active = true,
                Created = DateTime.UtcNow
            };
        }
    }
}
