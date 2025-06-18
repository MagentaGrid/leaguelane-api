using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly LeaguelaneDbContext _context;

        public MealRepository( LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<List<Meal>> GetAllMealsAsync()
        {
            return await _context.Meals.Where(x => x.Active == true).ToListAsync();
        }

        public async Task<Meal> GetMealByIdAsync(int id)
        {
            return await _context.Meals.FindAsync(id);
        }

        public async Task<Meal> AddMealAsync(Meal meal)
        {
            await _context.Meals.AddAsync(meal);
            await _context.SaveChangesAsync();

            return meal;
        }

        public async Task<Meal> UpdateMealAsync(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();

            return meal;
        }
    }
}
