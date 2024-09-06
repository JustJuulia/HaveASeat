using System.Text.Json.Serialization;

namespace haveaseat.DTOs;

public class NewUserDTO
{
    [JsonConstructor] public NewUserDTO() {}
    
    public NewUserDTO(User user)
    {
        Email = user.Email;
        Password = user.Password;
        Name = user.Name;
        Surname = user.Surname;
        
    }
    public string Email { get; set; }
    public string Password { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }
}