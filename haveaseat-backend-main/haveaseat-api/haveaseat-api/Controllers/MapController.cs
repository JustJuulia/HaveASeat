using haveaseat.DTOs;
using haveaseat.Models;
using haveaseat.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace haveaseat_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MapController(IMapRepository mapRepository) : ControllerBase
{
    [HttpGet("getAllRooms")]
    [ProducesResponseType(typeof(List<RoomDTOCells>), 200)]
    public async Task<IActionResult> GetAllRooms()
    {
        var result = await mapRepository.GetAllRooms();
        return Ok(result);
    }
    
    [HttpGet("GetAllMap")]
    [ProducesResponseType(typeof(List<RoomDTO>), 200)]
    public async Task<IActionResult> GetAllMap()
    {
        var result = await mapRepository.GetAllMap();
        return Ok(result);
    }
    
    [HttpGet("getAllDesks")]
    [ProducesResponseType(typeof(List<RoomDTODesks>), 200)]
    public async Task<IActionResult> GetAllDesks()
    {
        var result = await mapRepository.GetAllDesks();
        return Ok(result);
    }
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
    [HttpPost("EditChairPositionByDeskPosition")]
    [ProducesResponseType(typeof(Boolean), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditChairPositionByDeskPosition(int positonX, int positonY, ChairPosition chairPosition)
    {
        Boolean result =await mapRepository.EditChairPositionByDeskPosition(positonX, positonY, chairPosition);
        if (result)
        {
            return Ok(result);
        }
        return BadRequest("Something went wrong!");
    }
}