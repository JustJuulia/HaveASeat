using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using haveaseat.Models;

namespace haveaseat.Entities;

public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    
    [MaxLength(45)]
    public string Email { get; set; }
    
    [MaxLength(255)]
    public string Password { get; set; }

    [MaxLength(60)]
    public string Name { get; set; }
    [MaxLength(60)]
    public string Surname { get; set; }
   
    [Required]
    [MaxLength(255)]
    public string salt { get; set; }
    public Role Role { get; set; } 
    public ICollection<Reservation> Reservations { get; }
}