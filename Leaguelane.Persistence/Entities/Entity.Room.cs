using Leaguelane.Constants.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities;

public class Room : Entity
{
    public int RoomId { get; set; }
    public int RoomNumber { get; set; }

    [ForeignKey("RoomType")]
    public int RoomTypeId { get; set; }

    public RoomStatus Status { get; set; }

    //Navigation property
    public RoomType RoomType { get; set; }

    public string RoomName { get; set; }
}