using haveaseat.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace haveaseat_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AreaController(DataContext _context) : ControllerBase
{
    [HttpGet("GetArea")]
    [ProducesResponseType(typeof(Area), 200)]
    public async Task<IActionResult> GetArea()
    {
        Area? result = await _context.Area.FirstOrDefaultAsync();
        return Ok(result);
    }
}