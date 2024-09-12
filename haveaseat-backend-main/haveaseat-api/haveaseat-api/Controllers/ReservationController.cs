using haveaseat.DTOs;
using haveaseat.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace haveaseat.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController(IReservationRepository _reservationRepository) : ControllerBase
{
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

    [HttpPost("newReservation")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> InsertReservation(NewReservationDTO reservation)
    {
        NewReservationDTO result = await _reservationRepository.InsertReservations(reservation);
        return Created("Reservation added", result);
    }
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
    [HttpGet("getAllReservationsByDeskId/{id}")]
    [ProducesResponseType(typeof(List<LongTimeReservationToCheckDTQ>), 200)]
    

    public async Task<IActionResult> GetAllReservationsByDeskId(long id)
    {
        List<LongTimeReservationToCheckDTQ> longTimeReservationToCheckDTQs = await _reservationRepository.longTimeReservationToCheckDTQByDeskId(id);
        return Ok(longTimeReservationToCheckDTQs);
    }
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