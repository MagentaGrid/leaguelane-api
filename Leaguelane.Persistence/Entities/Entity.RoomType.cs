namespace Leaguelane.Persistence.Entities
{
    public class RoomType:Entity
    {
        public int RoomTypeID { get; set; }

        public string Name { get; set; }

        public int BaseOccupancy { get; set; } 

        public int MaxOccupancy { get; set; } 

        public int MaxChildren { get; set; } 

        public int MaxAdults { get; set; }

        public decimal Price { get; set; }
    }
}
