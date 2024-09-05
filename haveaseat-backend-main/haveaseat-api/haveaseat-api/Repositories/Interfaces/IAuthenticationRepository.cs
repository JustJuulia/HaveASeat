using haveaseat.DTOs;

namespace haveaseat.Repositories.Interfaces;

public interface IAuthenticationRepository
{
    Task<string> RegisterUser(NewUserDTO user, string Salt);

    Task<string> GetSaltByEmail(string email); //don't allow any client to get this information
    Task<UserDTO> GetUserByEmail(string email);
    Task<UserDTO> GetUserById(long id);

    Task<Boolean> LoginUser(NewUserDTO user);
}