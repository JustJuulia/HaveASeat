using haveaseat.Models;

namespace haveaseat.DTOs;

public class UserDTO
{
    public UserDTO(User user)
    {
        Id = user.Id;
        Email = user.Email;
        Role = user.Role;
    }
    
    public long Id { get; set; }
    
    public string Email { get; set; }

    public Role Role { get; set; } 
}