using Leaguelane.Enums.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class MealDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public MealType MealType { get; set; }
        public string ImageUrl { get; set; }
    }

    public class MealResponseDto : MealDto
    {
    }

    public class  MealResponse : Response
    {
        public MealResponseDto? Meal { get; set; }
    }

    public class MealListResponse : Response
    {
        public List<MealResponseDto> Meals { get; set; }
    }

    public class MealRequestDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public MealType MealType { get; set; }
        public IFormFile Image { get; set; }
    }
}
