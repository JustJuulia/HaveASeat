using haveaseat.Models;

namespace haveaseat.DTOs;
/// <summary>
/// Data Transfer Object (DTO) for User entity.
/// </summary>
/// <remarks>
/// Used for sending data to client about the user.
/// </remarks>
/// <seealso cref="Reservation"/>
public class UserDTO
{
    /// <summary>
    /// Initializes a new instance of the UserDTO class with the specified user.
    /// </summary>
    /// <param name="user">The User entity to initialize the DTO from.</param>
    /// <seealso cref="User"/>
    public UserDTO(User user)
    {
        Id = user.Id;
        Email = user.Email;
        Name = user.Name;
        Surname = user.Surname;
        Role = user.Role;
    }
    /// <summary>
    /// Gets or sets the id of the user.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the surname of the user.
    /// </summary>
    public string Surname { get; set; }
    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public Role Role { get; set; } 
}