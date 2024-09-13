using haveaseat.DTOs;
using haveaseat.Models;
using haveaseat.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace haveaseat.Controllers;

/// <summary>
/// This controller manages operations related to the map, including adding, deleting, and retrieving map data.
/// </summary>
/// <seealso cref="Cell"/>
/// <seealso cref="Room"/>
/// <seealso cref="Desk"/>
/// <seealso cref="IMapRepository"/>
/// <param name="mapRepository">The IMapRepository instance used for accessing methods to manipulate map data.</param>
[Route("api/[controller]")]
[ApiController]
public class MapController(IMapRepository mapRepository) : ControllerBase
{
    /// <summary>
    /// This task retrieves all rooms via an HTTP GET request.
    /// </summary>
    /// <seealso cref="RoomDTOCells"/>
    /// <returns>
    /// Returns an OK status and a list of RoomDTOCells objects.
    /// </returns>
    [HttpGet("getAllRooms")]
    [ProducesResponseType(typeof(List<RoomDTOCells>), 200)]
    public async Task<IActionResult> GetAllRooms()
    {
        var result = await mapRepository.GetAllRooms();
        return Ok(result);
    }
    /// <summary>
    /// This task retrieves the entire map via an HTTP GET request.
    /// </summary>
    /// <seealso cref="RoomDTO"/>
    /// <returns>
    /// Returns an OK status and a list of RoomDTO objects.
    /// </returns>
    [HttpGet("GetAllMap")]
    [ProducesResponseType(typeof(List<RoomDTO>), 200)]
    public async Task<IActionResult> GetAllMap()
    {
        var result = await mapRepository.GetAllMap();
        return Ok(result);
    }
    /// <summary>
    /// This task retrieves all desks via an HTTP GET request.
    /// </summary>
    /// <seealso cref="RoomDTODesks"/>
    /// <returns>
    /// Returns an OK status and a list of RoomDTODesks objects.
    /// </returns>
    [HttpGet("getAllDesks")]
    [ProducesResponseType(typeof(List<RoomDTODesks>), 200)]
    public async Task<IActionResult> GetAllDesks()
    {
        var result = await mapRepository.GetAllDesks();
        return Ok(result);
    }
    /// <summary>
    /// This task adds a new desk with the data provided from the HTTP POST request.
    /// </summary>
    /// <seealso cref="NewDeskDTO"/>
    /// <param name="newDeskDTO">The NewDeskDTO object containing the desk's details.</param>
    /// <returns>
    /// Returns a Created status and true if the desk was added to the database,
    /// a BadRequest status if the NewDeskDTO wasn't sent,
    /// a BadRequest status if the position doesn't exist or is already occupied,
    /// or an InternalServerError status if the record wasn't added to the database despite all requirements being met.
    /// </returns>
    [HttpPost("AddNewDesk")]
    [ProducesResponseType(typeof(Boolean), 201)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddNewDesk(NewDeskDTO newDeskDTO)
    {
        Cell cell = await mapRepository.GetCellByPosition(newDeskDTO.PositionX, newDeskDTO.PositionY);
        if (cell == null)
        {

            return BadRequest("This position doesn't exist");
        }
        
        Boolean result = await mapRepository.AddNewDesk(newDeskDTO,cell);
        if (result == false)
        {
            return BadRequest("This position is already occupied");
        }
        return Created("Desk has been succesfully added",result);
    }
    /// <summary>
    /// This task edits the chair position by desk position with the data provided from the HTTP POST request.
    /// </summary>
    /// <seealso cref="ChairPosition"/>
    /// <param name="positionX">The X position of the desk.</param>
    /// <param name="positionY">The Y position of the desk.</param>
    /// <param name="chairPosition">The new chair position.</param>
    /// <returns>
    /// Returns an OK status and true if the chair position was updated,
    /// or a BadRequest status if the operation failed.
    /// </returns>
    [HttpPost("EditChairPositionByDeskPosition")]
    [ProducesResponseType(typeof(Boolean), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditChairPositionByDeskPosition(int positionX, int positionY, ChairPosition chairPosition)
    {
        Boolean result =await mapRepository.EditChairPositionByDeskPosition(positionX, positionY, chairPosition);

        if (result)
        {
            return Ok(result);
        }
        return BadRequest("Something went wrong!");
    }
    /// <summary>
    /// This task deletes a desk and reservations on that desk based on the provided details from the HTTP DELETE request.
    /// </summary>
    /// <seealso cref="NewDeskDTO"/>
    /// <param name="deskDTO">The NewDeskDTO object containing the desk's details.</param>
    /// <returns>
    /// Returns an OK status and true if the desk was deleted,
    /// a BadRequest status if the NewDeskDTO wasn't sent,
    /// or a BadRequest status if the operation failed.
    /// </returns>
    [HttpDelete("DeleteDesk")]
    [ProducesResponseType(typeof(Boolean), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteDesk(NewDeskDTO deskDTO)
    {
        if(deskDTO == null)
        {
            return BadRequest("not send!");
        }
        Boolean result= await mapRepository.DeleteDesk(deskDTO);
        if(result == false)
        {
            return BadRequest("something went wrong");
        }
        return Ok(result);
    }
}