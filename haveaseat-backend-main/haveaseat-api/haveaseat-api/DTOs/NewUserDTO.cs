using System.Text.Json.Serialization;

namespace haveaseat.DTOs;

public class NewUserDTO
{
    [JsonConstructor] public NewUserDTO() {}
    
    public NewUserDTO(User user)
    {
        Email = user.Email;
        Password = user.Password;
    }
    public string Email { get; set; }
    public string Password { get; set; }
}