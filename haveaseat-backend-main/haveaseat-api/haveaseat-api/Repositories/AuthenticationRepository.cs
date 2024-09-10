using haveaseat.DbContexts;
using haveaseat.DTOs;
using haveaseat.Models;
using haveaseat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace haveaseat.Repositories;

public class AuthenticationRepository(DataContext context) : IAuthenticationRepository
{
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
        await context.SaveChangesAsync();
        return user.Email;
    }
    public async Task<string> GetSaltByEmail(string email)
    {
        String? salt = (await context.Users.Where(x => x.Email == email).SingleOrDefaultAsync()).salt;
        if (salt == null)
        {
            return null;
        }
        return salt;
    }
    public async Task<UserDTO> GetUserByEmail(string email)
    {
        User? user = await context.Users.Where(user => user.Email == email).SingleOrDefaultAsync();

        if (user == null)
        {
            return null;
        }
        UserDTO userDto = new UserDTO(user);
        return userDto;
    }

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
    public async Task<Boolean> LoginUser(NewUserLoginDTO user)
    {

        if (user == null)
        {
            return false;
        }
        User? userchecked = await context.Users.Where(userchecked => userchecked.Password == user.Password).Where(userchecked => userchecked.Password == user.Password).SingleOrDefaultAsync();
        if (userchecked == null)
        {
            return false;
        }
        return true;
    }
}