using Leaguelane.Constants.Enums;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Models.Dtos;

public class RoomDto
{
    public int RoomId { get; set; }
    public int RoomNumber { get; set; }
    public int RoomTypeId { get; set; }


    public string RoomName { get; set; } = string.Empty;
    public string RoomType { get; set; } = string.Empty;
    public RoomStatus Status { get; set; }
    public bool? Active { get; set; }

    public DateTime? Created { get; set; }
}






public record RoomResponse
(
    bool IsSuccess,
    string? ErrorMessage,
    Room? Room
);

public class RoomsResponse : Response
{
    public List<RoomDto>? Rooms { get; set; }
}

public class RoomResponseDto
{
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public RoomStatus Status { get; set; }
    public bool? Active { get; set; }
    public DateTime? Created { get; set; }
}