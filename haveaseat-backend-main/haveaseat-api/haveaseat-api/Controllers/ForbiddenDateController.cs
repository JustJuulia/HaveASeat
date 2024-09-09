namespace haveaseat.Controllers;
using haveaseat.DTOs;
using haveaseat.Repositories;
using haveaseat.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/[controller]")]
public class ForbiddenDateController(IForbiddenDateRepository forbiddenDateRepository) : ControllerBase
{
    [HttpPost("AddForbiddenDate")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddForbiddenDate(NewForbiddenDateDTO newForbiddenDate)
    {   if(newForbiddenDate == null)
        {
            return BadRequest("Not send!");
        }
        NewForbiddenDateDTO result = await forbiddenDateRepository.AddForbiddenDate(newForbiddenDate);
        return Created("Forbidden date added", result);
    }
    [HttpDelete("delete/{date}")]
    [ProducesResponseType(typeof(Boolean), 202)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteForbiddenDateByDate(DateOnly date)
    {
        if(date == null){
            return BadRequest("not send!");

        }
        Boolean result = await forbiddenDateRepository.DeleteForbiddenDateByDate(date);
        if (!result)
        {
            return BadRequest("Something went wrong");
        }
        return Accepted("delete Forbidden date",result);
    }
    [HttpGet("getAllForbiddenDates")]
    [ProducesResponseType(typeof(List<ForbiddenDateDTO>), 200)]
    public async Task<IActionResult> GetAllDesks()
    {
        var result = await forbiddenDateRepository.GetAllForbiddenDates();
        return Ok(result);
    }

    [HttpGet("GetByDate/{date}")]
    [ProducesResponseType(typeof(ForbiddenDateDTO), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForbiddenDateByDate(DateOnly date)
    {
        var getForbiddenDateByDate = await forbiddenDateRepository.GetForbiddenDateByDate(date);
        if (getForbiddenDateByDate == null)
        {
            return NotFound(new { Message = "Date is not Forbidden date!", ForbiddenDate = date });
        }
        return Ok(getForbiddenDateByDate);
    }

    [HttpGet("GetById/{id}")]
    [ProducesResponseType(typeof(UserDTO), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForbiddenDateById(long id)
    {
        var getForbiddenDateById = await forbiddenDateRepository.GetForbiddenDateById(id);
        if (getForbiddenDateById == null)
        {
            return NotFound(new { Message = "Date is not Forbidden date!", GivenId = id });
        }

        return Ok(getForbiddenDateById);
    }
    [HttpPost("EditForbiddenDate")]
    [ProducesResponseType(typeof(Boolean), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditForbiddenDateByDate(NewForbiddenDateDTO newForbiddenDate)
    {
        if(newForbiddenDate == null)
        {
            return BadRequest("not send!");
        }
        Boolean updated = await forbiddenDateRepository.EditForbiddenDateByDate(newForbiddenDate);
        if (!updated)
        {
            return BadRequest("Something went wrong");
        }
        return Ok(updated);
    }

}
