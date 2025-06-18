using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class RoomTypeDto
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public int BaseOccupancy { get; set; }
        public int MaxOccupancy { get; set; }
        public int MaxChildren { get; set; }
        public int MaxAdults { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }

    public record RoomTypeResponse(
         bool IsSuccess,
         string? Message,
         RoomType? User
    );

    public class RoomTypesResponse : Response
    {
        public List<RoomTypeDto>? RoomTypes { get; set; }
    }

    public class RoomTypeResponseDto
    {
        public int RoomTypeId { get; set; }
        public string Name { get; set; }
        public int BaseOccupancy { get; set; }
        public int MaxOccupancy { get; set; }
        public int MaxChildren { get; set; }
        public int MaxAdults { get; set; }
        public decimal Price { get; set; }
    }
}
