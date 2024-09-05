using haveaseat.DbContexts;
using haveaseat.DTOs;
using haveaseat.Models;
using haveaseat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace haveaseat.Repositories;

public class AuthenticationRepository(DataContext context) : IAuthenticationRepository
{
    public async Task<NewUserDTO> RegisterUser(NewUserDTO user)
    {
        User entry = new User
        {
            Email = user.Email,
            Password = user.Password,
            Role = Role.EMPLOYEE
        };
        await context.Users.AddAsync(entry);
        await context.SaveChangesAsync();
        return user;
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
    
}