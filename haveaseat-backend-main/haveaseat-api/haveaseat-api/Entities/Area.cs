using Microsoft.EntityFrameworkCore;

namespace haveaseat.Entities;
/// <summary>
/// This entity represents an area with specific dimensions.
/// </summary>
/// <remarks>
/// This is the space of the office map.
/// 
/// This class is used for creating a table in the postgresql database in which variables from this class are rows.
/// </remarks>
public class Area
{
    /// <summary>
    /// Gets or sets the unique identifier for the area.
    /// </summary>
    public short Id { get; set; }
    /// <summary>
    /// Gets or sets the height of the area.
    /// </summary>
    /// <remarks>
    /// Default value is 11.
    /// </remarks>
    public int Height { get; set; } = 11;
    /// <summary>
    /// Gets or sets the width of the area.
    /// </summary>
    /// <remarks>
    /// Default value is 15.
    /// </remarks>
    public int Width { get; set; } = 15;
}