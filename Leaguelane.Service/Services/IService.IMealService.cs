using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IMealService
    {
        Task<List<Meal>> GetAllMealsAsync();
        Task<Meal> GetMealByIdAsync(int id);
        Task<Meal> AddMealAsync(Meal meal);
        Task<Meal> UpdateMealAsync(Meal meal);
    }
}
