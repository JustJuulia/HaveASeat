using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace haveaseat.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for user login information.
/// </summary>
/// <remarks>
/// Used for getting user login data from the client.
/// </remarks>
/// <seealso cref="User"/>
public class NewUserLoginDTO
{
    /// <summary>
    /// Initializes a new instance of the NewUserLoginDTO class.
    /// </summary>
    /// <remarks>
    /// It is needed to be able to get data from client sent in JSON to the NewUserLoginDTO object.
    /// </remarks>
    [JsonConstructor]
    public NewUserLoginDTO() { }

    /// <summary>
    /// Initializes a new instance of the NewUserLoginDTO class with the specified user.
    /// </summary>
    /// <param name="user">The User entity to initialize the DTO from.</param>
    /// <seealso cref="User"/>
    public NewUserLoginDTO(User user)
    {
        Email = user.Email;
        Password = user.Password;
    }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; }
}