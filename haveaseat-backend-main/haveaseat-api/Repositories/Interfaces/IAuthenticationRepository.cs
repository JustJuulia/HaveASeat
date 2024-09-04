using haveaseat.DTOs;

namespace haveaseat.Repositories.Interfaces;

public interface IAuthenticationRepository
{
    Task<NewUserDTO> RegisterUser(NewUserDTO user);
    Task<UserDTO> GetUserByEmail(string email);
    Task<UserDTO> GetUserById(long id);
}