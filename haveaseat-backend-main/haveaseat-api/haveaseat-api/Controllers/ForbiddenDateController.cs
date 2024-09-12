namespace haveaseat.Controllers;
using haveaseat.DTOs;
using haveaseat.Repositories;
using haveaseat.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// This controller handles forbidden date related operations such as adding, editing, deleting and retrieving forbidden date information.
/// </summary>
/// <seealso cref="ForbiddenDate"/>
/// <seealso cref="ForbiddenDateRepository"/>
/// <param name="forbiddenDateRepository">The IForbiddenDateRepository instance used for accessing methods to manipulate forbidden date data.</param>
[ApiController]
[Route("api/[controller]")]
public class ForbiddenDateController(IForbiddenDateRepository forbiddenDateRepository) : ControllerBase
{
    /// <summary>
    /// This task adds a forbidden date with the data provided from the HTTP POST request.
    /// </summary>
    /// <seealso cref="NewForbiddenDateDTO"/>
    /// <param name="newForbiddenDate">The ForbiddenDate object containing the details of the forbidden date to be added.</param>
    /// <returns>
    /// Returns a Created status if all requiments are met,
    /// a BadRequest status if NewForbiddenDateDTO wasn't send,
    /// a BadRequest status if forbidden date already exist,
    /// an InternalServerError status if the record wasn't added to the database despite all requirements being met,
    /// </returns>
    [HttpPost("AddForbiddenDate")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddForbiddenDate(NewForbiddenDateDTO newForbiddenDate)
    {   if(newForbiddenDate == null)
        {
            return BadRequest("Not send!");
        }
        if (await forbiddenDateRepository.GetForbiddenDateByDate(newForbiddenDate.Date) != null)
        {
            return BadRequest("Date already exist");
        }
        
        NewForbiddenDateDTO result = await forbiddenDateRepository.AddForbiddenDate(newForbiddenDate);
        if (result == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error with database");
        }
        return Created("Forbidden date added", result);
    }
    /// <summary>
    /// This task deletes forbidden date based on the provided date from the HTTP Delete request.
    /// </summary>
    /// <param name="date">The date to be deleted.</param>
    /// <returns>
    /// Returns status Accepted and true if all requirements are met,
    /// a BadRequest if date wasn't send,
    /// a BadRequest if date isn't forbidden,
    /// an InternalServerError 
    /// </returns>
    [HttpDelete("delete/{date}")]
    [ProducesResponseType(typeof(Boolean), 202)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteForbiddenDateByDate(DateOnly date)
    {
        if(date == null){
            return BadRequest("not send!");

        }
        if (await forbiddenDateRepository.GetForbiddenDateByDate(date) == null)
        {
            return BadRequest("This date is not forbidden!");
        }
        Boolean result = await forbiddenDateRepository.DeleteForbiddenDateByDate(date);
        if (!result)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,"error with database!");
        }
        return Accepted("delete Forbidden date",result);
    }
    
    [HttpGet("getAllForbiddenDates")]
    [ProducesResponseType(typeof(List<ForbiddenDateDTO>), 200)]
    public async Task<IActionResult> GetAllForbiddenDates()
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditForbiddenDateByDate(NewForbiddenDateDTO newForbiddenDate)
    {
        if(await forbiddenDateRepository.GetForbiddenDateByDate(newForbiddenDate.Date) == null)
        {
            return BadRequest("Date is not forbidden");
        }
        if(newForbiddenDate == null)
        {
            return BadRequest("not send!");
        }
        Boolean updated = await forbiddenDateRepository.EditForbiddenDateByDate(newForbiddenDate);
        if (!updated)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error with database");
        }
        return Ok(updated);
    }

}
