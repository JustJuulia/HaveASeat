using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace haveaseat.Entities;

public class Cell
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    [MaxLength(50)]
    public string Border { get; set; } = "none";
    public Room Room { get; set; }
    public long RoomId { get; set; }
}