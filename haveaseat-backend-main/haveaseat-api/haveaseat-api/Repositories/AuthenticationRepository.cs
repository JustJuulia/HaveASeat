using haveaseat.DbContexts;
using haveaseat.DTOs;
using haveaseat.Models;
using haveaseat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace haveaseat.Repositories;
/// <summary>
/// This repository stores authentication-related methods.
/// </summary>
/// <seealso cref="Role"/>
/// <seealso cref="IAuthenticationRepository"/>
/// <seealso cref="DataContext"/>
/// <param name="context">The DataContext instance used for accessing the database.</param>
public class AuthenticationRepository(DataContext context) : IAuthenticationRepository
{
    /// <summary>
    /// This task registers a new user.
    /// </summary>
    /// <seealso cref="NewUserDTO"/>
    /// <param name="user">The NewUserDTO object containing the user's registration details.</param>
    /// <param name="Salt">The salt to be used for hashing the user's password.</param>
    /// <returns>Returns the email of the registered user.</returns>
    public async Task<string> RegisterUser(NewUserDTO user, string Salt)
    {
        User entry = new User
        {
            Email = user.Email,
            Password = user.Password,
            Name = user.Name,
            Surname = user.Surname,
            Role = Role.EMPLOYEE,
            salt=Salt
        };
        await context.Users.AddAsync(entry);
        int result = await context.SaveChangesAsync();
        if (result > 0)
        {
            return user.Email;
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// This task retrieves the salt associated with a given email.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>Returns the salt as a string, or null if the user is not found.</returns>
    /// <remarks>
    /// This task should't be added to controller.
    /// </remarks>
    /// <remarks>It is case insensitive.</remarks>
    public async Task<string> GetSaltByEmail(string email)
    {
        String? salt = (await context.Users.Where(x => x.Email.ToLower() == email.ToLower()).SingleOrDefaultAsync()).salt;
        if (salt == null)
        {
            return null;
        }
        return salt;
    }
    /// <summary>
    /// This task retrieves a user by their email.
    /// </summary>
    /// <seealso cref="UserDTO"/>
    /// <param name="email">The email of the user.</param>
    /// <returns>Returns a UserDTO object if the user is found, or null if the user is not found.</returns>
    /// <remarks>It is case insensitive.</remarks>
    public async Task<UserDTO> GetUserByEmail(string email)
    {
        User? user = await context.Users.Where(user => user.Email.ToLower() == email.ToLower()).SingleOrDefaultAsync();

        if (user == null)
        {
            return null;
        }
        UserDTO userDto = new UserDTO(user);
        return userDto;
    }
    /// <summary>
    /// This task retrieves a user by their ID.
    /// </summary>
    /// <seealso cref="UserDTO"/>
    /// <param name="id">The ID of the user.</param>
    /// <returns>Returns a UserDTO object if the user is found, or null if the user is not found.</returns>
    public async Task<UserDTO> GetUserById(long id)
    {
        User? user = await context.Users.Where(user => user.Id == id).SingleOrDefaultAsync();
        if (user == null)
        {
            return null;
        }
        UserDTO userDto = new UserDTO(user);
        return userDto;
    }
    /// <summary>
    /// This task logs in a user.
    /// </summary>
    /// <seealso cref="NewUserLoginDTO"/>
    /// <param name="user">The NewUserLoginDTO object containing the user's login details.</param>
    /// <returns>Returns true if the login is successful, or false if the login fails.</returns>
    public async Task<Boolean> LoginUser(NewUserLoginDTO user)
    {

        if (user == null)
        {
            return false;
        }
        User? userchecked = await context.Users.Where(userchecked => userchecked.Email.ToLower() == user.Email.ToLower()).Where(userchecked => userchecked.Password == user.Password).SingleOrDefaultAsync();
        if (userchecked == null)
        {
            return false;
        }
        return true;
    }
}