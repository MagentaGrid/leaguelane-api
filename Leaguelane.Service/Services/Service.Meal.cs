using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class MealService: IMealService
    {
        private readonly IMealRepository _mealRepository;

        public MealService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public async Task<List<Meal>> GetAllMealsAsync()
        {
            return await _mealRepository.GetAllMealsAsync();
        }
        public async Task<Meal> GetMealByIdAsync(int id)
        {
            return await _mealRepository.GetMealByIdAsync(id);
        }
        public async Task<Meal> AddMealAsync(Meal meal)
        {
            return await _mealRepository.AddMealAsync(meal);
        }
        public async Task<Meal> UpdateMealAsync(Meal meal)
        {
            return await _mealRepository.UpdateMealAsync(meal);
        }
    }
}
