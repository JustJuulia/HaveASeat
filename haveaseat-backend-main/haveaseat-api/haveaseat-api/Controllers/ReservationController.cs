using haveaseat.DTOs;
using haveaseat.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace haveaseat.Controllers;

/// <summary>
/// This controller manages reservation operations, including creating, deleting, and retrieving reservations and related user data.
/// </summary>
/// <seealso cref="Reservation"/>
/// <seealso cref="User"/>
/// <seealso cref="IReservationRepository"/>
/// <param name="_reservationRepository">The IReservationRepository instance used for accessing methods to manipulate reservation data. </param>
[ApiController]
[Route("api/[controller]")]
public class ReservationController(IReservationRepository _reservationRepository) : ControllerBase
{
    /// <summary>
    /// This task retrieves reservations by user email via an HTTP GET request.
    /// </summary>
    /// <seealso cref="ReservationDTO"/>
    /// <param name="email">The email address of the user to retrieve reservations for.</param>
    /// <returns>
    /// Returns an OK status and a list of ReservationDTO objects if reservations exist,
    /// or a NotFound status if no reservations are found for the given user.
    /// </returns>
    [HttpGet("getByEmail/{email}")]
    [ProducesResponseType(typeof(List<ReservationDTO>), 200)]
    public async Task<IActionResult> ReservationsByEmail(string email)
    {
        List<ReservationDTO> result = await _reservationRepository.GetReservationsByUserEmail(email);
        if (!result.Any())
        {
            return NotFound(new { Message = "No reservations for given user", User = email });
        }
        return Ok(result);
    }
    /// <summary>
    /// This task retrieves reservations by date via an HTTP GET request.
    /// </summary>
    /// <seealso cref="ReservationDTO"/>
    /// <param name="date">The date to retrieve reservations for.</param>
    /// <returns>
    /// Returns an OK status and a list of ReservationDTO objects.
    /// </returns>
    [HttpGet("getByDay/{date}")]
    [ProducesResponseType(typeof(List<ReservationDTO>), 200)]
    public async Task<IActionResult> ReservationsByDate(DateOnly date)
    {
        List<ReservationDTO> result = await _reservationRepository.GetReservationsByDay(date);
        if (!result.Any())
        {
            return Ok(new List<ReservationDTO>());
        }
        return Ok(result);
    }
    /// <summary>
    /// This task adds a new reservation with the data provided from the HTTP POST request.
    /// </summary>
    /// <seealso cref="NewReservationDTO"/>
    /// <param name="reservation">The NewReservationDTO object containing the reservation details.</param>
    /// <returns>
    /// Returns a Created status if the reservation was added to the database,
    /// a BadRequest status if the NewReservationDTO wasn't sent,
    /// or a BadRequest status if the reservation already exists.
    /// </returns>
    [HttpPost("newReservation")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> InsertReservation(NewReservationDTO reservation)
    {
        if (reservation == null)
        {
            return BadRequest("Not send!");
        }
        if(await _reservationRepository.CheckIfReservationExistByDateAnDeskId(reservation.Date, reservation.DeskId)==true)
        {
            return BadRequest("Reservation already exist!");
        }
        NewReservationDTO result = await _reservationRepository.InsertReservations(reservation);
        return Created("Reservation added", result);
    }
    /// <summary>
    /// This task retrieves all users with reservations on a specific date via an HTTP GET request.
    /// </summary>
    /// <seealso cref="UserDTO"/>
    /// <param name="date">The date to retrieve users with reservations for.</param>
    /// <returns>
    /// Returns an OK status and a list of UserDTO objects,
    /// or a BadRequest status if the date wasn't sent.
    /// </returns>
    [HttpGet("getAllUsersWithReservationByDay/{date}")]
    [ProducesResponseType(typeof(List<UserDTO>), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllUsersFromReservationsByDate(DateOnly date)
    {
        if (date == null)
        {
            return BadRequest("Not send!");
        }
        List<UserDTO> userDTOs = await _reservationRepository.GetAllUsersFromReservationsByDate(date);
        return Ok(userDTOs);
    }
    /// <summary>
    /// This task retrieves all reservations by desk ID via an HTTP GET request.
    /// </summary>
    /// <seealso cref="LongTimeReservationToCheckDTO"/>
    /// <param name="id">The ID of the desk to retrieve reservations for.</param>
    /// <returns>
    /// Returns an OK status and a list of LongTimeReservationToCheckDTQ objects.
    /// </returns>
    [HttpGet("getAllReservationsByDeskId/{id}")]
    [ProducesResponseType(typeof(List<LongTimeReservationToCheckDTO>), 200)]
    

    public async Task<IActionResult> GetAllReservationsByDeskId(long id)
    {
        List<LongTimeReservationToCheckDTO> longTimeReservationToCheckDTQs = await _reservationRepository.longTimeReservationToCheckDTQByDeskId(id);
        return Ok(longTimeReservationToCheckDTQs);
    }
    /// <summary>
    /// This task deletes a reservation based on the provided ID from the HTTP DELETE request.
    /// </summary>
    /// <param name="id">The ID of the reservation to delete.</param>
    /// <returns>
    /// Returns an Accepted status if the reservation was deleted,
    /// or a BadRequest status if the operation failed.
    /// </returns>
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(typeof(Boolean), 202)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteReservation(long id)
    {
        Boolean result = await _reservationRepository.DeleteReservationById(id);
        if (!result)
        {
            return BadRequest("Something went wrong");
        }
        return Accepted(result);
    }

} 