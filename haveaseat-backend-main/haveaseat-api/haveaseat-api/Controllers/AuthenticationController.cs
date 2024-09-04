using haveaseat.DTOs;
using haveaseat.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace haveaseat_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IAuthenticationRepository authenticationRepository) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUser(NewUserDTO newUser)
    {
        try
        {
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            NewUserDTO result = await authenticationRepository.RegisterUser(newUser);
            return Created("User registered", result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpGet("GetByEmail/{email}")]
    [ProducesResponseType(typeof(UserDTO), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var userByEmail = await authenticationRepository.GetUserByEmail(email);
        if (userByEmail == null)
        {
            return NotFound(new { Message = "No such user", User = email });
        }
        return Ok(userByEmail);
    }

    [HttpGet("GetById/{id}")]
    [ProducesResponseType(typeof(UserDTO), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(long id)
    {
        var userById = await authenticationRepository.GetUserById(id);
        if (userById == null)
        {
            return NotFound(new { Message = "No such user", GivenId = id });
        }

        return Ok(userById);
    }
}