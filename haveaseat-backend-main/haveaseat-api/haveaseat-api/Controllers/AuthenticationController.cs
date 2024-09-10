using haveaseat.DTOs;
using haveaseat.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Runtime.InteropServices;

namespace haveaseat_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IAuthenticationRepository authenticationRepository) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(NewUserDTO),201)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser(NewUserDTO newUser)
    {
        if (await authenticationRepository.GetUserByEmail(newUser.Email) != null) {
            return BadRequest("User already exist !"+(authenticationRepository.GetUserByEmail(newUser.Email)));
        }
        try
        {
            String salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password, salt);
            string result = await authenticationRepository.RegisterUser(newUser, salt);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error with adding user to database!");
            }
            return Created("User registered", newUser);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An server side error");
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
    [HttpPost("Login")]
    [ProducesResponseType(typeof(Boolean), 202)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginUser(NewUserLoginDTO user)
    {
        if (user == null)
        {
            return BadRequest("Not send!");
        }
        try
        {
            if(await authenticationRepository.GetUserByEmail(user.Email) == null)
            {
                return BadRequest("Wrong email or password!");
            }
            String salt = await authenticationRepository.GetSaltByEmail(user.Email);
            if (salt == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error with connecting to database!");
            }
           
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
            Boolean result = await authenticationRepository.LoginUser(user);
           
            if(result == false)
            {
                return BadRequest("Wrong email or password!");
            }
            return Accepted("Login successful", true);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An server side error");
            Console.WriteLine(e);
            throw;
        }
        
    }
}