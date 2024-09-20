using haveaseat.DTOs;
using haveaseat.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace haveaseat.Controllers;

/// <summary>
/// This controller handles user-related operations such as registration, login and retrieving user informations.
/// </summary>
/// <seealso cref="User"/>
/// <seealso cref="IAuthenticationRepository"/>
/// <param name="authenticationRepository">The IAuthenticationRepository instance used for accessing methods to manipulate user data.</param>
[ApiController]
[Route("api/[controller]")]
public class AuthenticationController(IAuthenticationRepository authenticationRepository) : ControllerBase
{
    /// <summary>
    /// This tasks registers a user with the data provided from the HTTP POST request.
    /// </summary>
    /// <seealso cref="NewUserDTO"/>
    /// <param name="newUser">The NewUserDTO object containing the user's registration details.</param>
    /// <returns>
    /// Returns a Created status and NewUserDTO object if user was added to database,
    /// a BadRequest status if the user already exists,
    /// or an InternalServerError status if the record wasn't added to the database despite all requirements being met.
    /// </returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(NewUserDTO),201)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser(NewUserDTO newUser)
    {
        if (await authenticationRepository.GetUserByEmail(newUser.Email) != null) {
            return BadRequest(new { error = "User already exist!" });
        }

            String salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password, salt);
            string result = await authenticationRepository.RegisterUser(newUser, salt);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error with adding user to database!" });
            }
            return Created("User registered", newUser);
        
    }
    /// <summary>
    /// This task retrieves user information based on the provided email from the HTTP GET request.
    /// </summary>
    /// <seealso cref="UserDTO"/>
    /// <param name="email">The email address of the user to retrieve.</param>
    /// <returns>
    /// Returns a OK status and UserDTO object if the user exist in the database,
    /// or a NotFound status if user doesn't exist in database.
    /// </returns>
    /// <remarks>It is case insensitive.</remarks>
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
    /// <summary>
    /// This task retrives user information based on the provided id from the HTTP GET request.
    /// </summary>
    /// <seealso cref="UserDTO"/>
    /// <param name="id">The id of the user to retrieve.</param>
    /// <returns>
    /// Returns an Ok status and UserDTO object if the user exists in the database,
    /// or a NotFound status if the user doesn't exist in the database.
    /// </returns>
    [HttpGet("GetById/{id}")]
    [ProducesResponseType(typeof(UserDTO), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(long id)
    {
        var userById = await authenticationRepository.GetUserById(id);
        if (userById == null)
        {
            return NotFound(new { Message = "No such user", userId = id });
        }

        return Ok(userById);
    }
    /// <summary>
    /// This tasks logins a user with the data provided from the HTTP POST request.
    /// </summary>
    /// <seealso cref="NewUserLoginDTO"/>
    /// <param name="user">The NewUserLoginDTO object containing the user's login details.</param>
    /// <returns>
    /// Returns a Accepted status and true if user is successfully logged in,
    /// a BadRequest status if NewUserLoginDTO wasn't sent,
    /// a BadRequest status if the user already exists or the credentials are incorrect,
    /// or an InternalServerError status if the record wasn't added to the database despite all requirements being met.
    /// </returns>
    [HttpPost("Login")]
    [ProducesResponseType(typeof(Boolean), 202)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginUser(NewUserLoginDTO user)
    {
        if (user == null)
        {
            return BadRequest(new { error = "Not sent!" });
        }
      
        
           if(await authenticationRepository.GetUserByEmail(user.Email) == null)
            {
                return BadRequest(new { error = "Wrong email or password!" });
            }
            String salt = await authenticationRepository.GetSaltByEmail(user.Email);
            if (salt == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error with connecting to database!" });
            }
           
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
            Boolean result = await authenticationRepository.LoginUser(user);
           
            if(result == false)
            {
                return BadRequest(new { error = "Wrong email or password!" });
            }
            return Accepted("Login successful", true);
        
        
    }
}