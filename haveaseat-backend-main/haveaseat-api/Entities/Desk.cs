using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using haveaseat.Models;

namespace haveaseat.Entities;

public class Desk
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    
    public ChairPosition ChairPosition { get; set; }
    public ICollection<Reservation> Reservations { get; }
    
    public long RoomId { get; set; }
    public Room Room { get; set; }

}