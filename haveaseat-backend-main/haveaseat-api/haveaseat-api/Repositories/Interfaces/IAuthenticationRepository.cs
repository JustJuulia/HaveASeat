using haveaseat.DTOs;

namespace haveaseat.Repositories.Interfaces;

/// <summary>
/// This interface defines the methods for authentication-related operations.
/// </summary>
/// <seealso cref="AuthenticationRepository"/>
public interface IAuthenticationRepository
{
    /// <summary>
    /// This task registers a new user.
    /// </summary>
    /// <seealso cref="NewUserDTO"/>
    /// <param name="user">The NewUserDTO object containing the details of the user to be registered.</param>
    /// <param name="Salt">The salt used for hashing the user's password.</param>
    /// <returns>Returns a email of registered user.</returns>
    Task<string> RegisterUser(NewUserDTO user, string Salt);
    /// <summary>
    /// This task retrieves the salt by email.
    /// </summary>
    /// <param name="email">The email of the user whose salt is to be retrieved.</param>
    /// <returns>Returns a string representing the salt.</returns>
    /// <remarks>It is case insensitive.</remarks>
    Task<string> GetSaltByEmail(string email); //don't allow any client to get this information
    /// <summary>
    /// This task retrieves a user by email.
    /// </summary>
    /// <seealso cref="UserDTO"/>
    /// <param name="email">The email of the user to be retrieved.</param>
    /// <returns>Returns a UserDTO object.</returns>
    /// <remarks>It is case insensitive.</remarks>
    Task<UserDTO> GetUserByEmail(string email);
    /// <summary>
    /// This task retrieves a user by ID.
    /// </summary>
    /// <seealso cref="UserDTO"/>
    /// <param name="id">The ID of the user to be retrieved.</param>
    /// <returns>Returns a UserDTO object.</returns>
    
    Task<UserDTO> GetUserById(long id);
    /// <summary>
    /// This task logs in a user.
    /// </summary>
    /// <seealso cref="NewUserLoginDTO"/>
    /// <param name="user">The NewUserLoginDTO object containing the login details of the user.</param>
    /// <returns>Returns true if the login was successful, or false if it failed.</returns>
    Task<Boolean> LoginUser(NewUserLoginDTO user);
}