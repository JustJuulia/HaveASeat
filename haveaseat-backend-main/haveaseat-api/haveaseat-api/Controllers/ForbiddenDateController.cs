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
    /// <param name="newForbiddenDate">The NewForbiddenDateDTO object containing the details of the forbidden date to be added.</param>
    /// <returns>
    /// Returns a Created status if forbidden date was added to database,
    /// a BadRequest status if NewForbiddenDateDTO wasn't send,
    /// a BadRequest status if forbidden date already exist,
    /// or an InternalServerError status if the record wasn't added to the database despite all requirements being met.
    /// </returns>
    [HttpPost("AddForbiddenDate")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddForbiddenDate(NewForbiddenDateDTO newForbiddenDate)
    {   if(newForbiddenDate == null)
        {
            return BadRequest(new { error = "Not send!" });
        }
        if (await forbiddenDateRepository.GetForbiddenDateByDate(newForbiddenDate.Date) != null)
        {
            return BadRequest(new { error = "Date already is forbidden" });
        }
        
        NewForbiddenDateDTO result = await forbiddenDateRepository.AddForbiddenDate(newForbiddenDate);
        if (result == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An error with the database" });
        }
        return Created("Forbidden date added", result);
    }
    /// <summary>
    /// This task deletes forbidden date based on the provided date from the HTTP Delete request.
    /// </summary>
    /// <param name="date">The date to be deleted.</param>
    /// <returns>
    /// Returns status Accepted and true if forbidden date was deleted,
    /// a BadRequest if date wasn't send,
    /// a BadRequest if date isn't forbidden,
    /// or an InternalServerError if the forbidden date wasn't delete despites all requirements being met. 
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
    /// <summary>
    /// This task retrieves a list of all forbidden dates from the database by HTTP GET request.
    /// </summary>
    /// <seealso cref="ForbiddenDateDTO"/>    
    /// <returns>
    /// Returns an Ok status and list of ForbiddenDateDTO objects.
    /// </returns>
    [HttpGet("getAllForbiddenDates")]
    [ProducesResponseType(typeof(List<ForbiddenDateDTO>), 200)]
    public async Task<IActionResult> GetAllForbiddenDates()
    {
        var result = await forbiddenDateRepository.GetAllForbiddenDates();
        return Ok(result);
    }

    /// <summary>
    /// This task retrives forbidden date information based on the provided date from the HTTP GET request.
    /// </summary>
    /// <seealso cref="ForbiddenDateDTO"/>
    /// <param name="date">The date of the forbidden date to retrieve.</param>
    /// <returns>
    /// Rewturns an Ok status and ForbiddenDateDTO object if the forbidden date exist in te database,
    /// or a NotFound status if date isn't forbidden.
    /// </returns>
    [HttpGet("GetByDate/{date}")]
    [ProducesResponseType(typeof(ForbiddenDateDTO), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForbiddenDateByDate(DateOnly date)
    {
        var getForbiddenDateByDate = await forbiddenDateRepository.GetForbiddenDateByDate(date);
        if (getForbiddenDateByDate == null)
        {
            return NotFound(new { error = "Date is not Forbidden date!", forbiddenDate = date });
        }
        return Ok(getForbiddenDateByDate);
    }
    /// <summary>
    /// This task retrives forbidden date information based on the provided id from the HTTP GET request.
    /// </summary>
    /// <seealso cref="ForbiddenDateDTO"/>
    /// <param name="id">The id of the forbidden date to retrieve.</param>
    /// <returns>
    /// Rewturns an Ok status and ForbiddenDateDTO object if the forbidden date exist in te database,
    /// or a NotFound status if date isn't forbidden.
    /// </returns>
    [HttpGet("GetById/{id}")]
    [ProducesResponseType(typeof(ForbiddenDateDTO), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetForbiddenDateById(long id)
    {
        var getForbiddenDateById = await forbiddenDateRepository.GetForbiddenDateById(id);
        if (getForbiddenDateById == null)
        {
            return NotFound(new { error = "Date is not Forbidden date!",forbiddenDateId = id });
        }

        return Ok(getForbiddenDateById);
    }
    /// <summary>
    /// This task edits forbidden date based on the provided date from the HTTP Post request.
    /// </summary>
    /// <seealso cref="NewForbiddenDateDTO"/>
    /// <param name="newForbiddenDate">The date to be edited.</param>
    /// <returns>
    /// Returns status Ok and true if all forbidden date was deleted,
    /// a BadRequest if NewForbiddenDateDTO object wasn't send,
    /// a BadRequest if date isn't forbidden,
    /// or an InternalServerError if the forbidden date wasn't edited despites all requirements being met. 
    /// </returns>
    [HttpPost("EditForbiddenDate")]
    [ProducesResponseType(typeof(Boolean), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditForbiddenDateByDate(NewForbiddenDateDTO newForbiddenDate)
    {
        if (newForbiddenDate == null)
        {
            return BadRequest(new { error = "Not send!" });
        }
        if (await forbiddenDateRepository.GetForbiddenDateByDate(newForbiddenDate.Date) == null)
        {
            return BadRequest(new { error = "Date is not forbidden" });
        }
        
        Boolean updated = await forbiddenDateRepository.EditForbiddenDateByDate(newForbiddenDate);
        if (!updated)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Error with the database" });
        }
        return Ok(updated);
    }

}
