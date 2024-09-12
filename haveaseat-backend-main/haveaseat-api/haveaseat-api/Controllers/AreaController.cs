using haveaseat.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace haveaseat_api.Controllers;


/// <summary>
/// This controller provides data from the Area table in the database.
/// </summary>
/// <seealso cref = "Area"/>
/// <seealso cref = "DataContext"/>
/// <param name="_context">The DataContext instance used to interact with the database.</param>
[ApiController]
[Route("api/[controller]")]
public class AreaController(DataContext _context) : ControllerBase
{
    /// <summary>
    /// This task provides Area object by http request at */GetArea
    /// </summary>
    /// 
    /// <returns>
    /// Returns object of the class Area.
    /// </returns>
    [HttpGet("GetArea")]
    [ProducesResponseType(typeof(Area), 200)]
    public async Task<IActionResult> GetArea()
    {
        Area? result = await _context.Area.FirstOrDefaultAsync();
        return Ok(result);
    }
}