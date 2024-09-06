using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace haveaseat.Entities;

public class Room
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    [MaxLength(4)]
    public string Name { get; set; }

    public ICollection<Desk>? Desks { get; }
    
    public ICollection<Cell> Cells { get; }
}
