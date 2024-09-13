using System.Text.Json.Serialization;

namespace haveaseat.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for User entity.
/// </summary>
/// <remarks>
/// Used for getting data from client for registration.
/// </remarks>
/// <seealso cref="User"/>
public class NewUserDTO
{
    /// <summary>
    /// Initializes a new instance of the NewUserDTO class.
    /// </summary>
    /// <remarks>
    /// It is needed to be able to get data from client send in JSON to the NewUserDTO object.
    /// </remarks>
    [JsonConstructor]
    public NewUserDTO() { }

    /// <summary>
    /// Initializes a new instance of the NewUserDTO class with the specified user.
    /// </summary>
    /// <seealso cref="User"/>
    /// <param name="user">The User entity to initialize the DTO from.</param>
    public NewUserDTO(User user)
    {
        Email = user.Email;
        Password = user.Password;
        Name = user.Name;
        Surname = user.Surname;
    }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the surname of the user.
    /// </summary>
    public string Surname { get; set; }
}