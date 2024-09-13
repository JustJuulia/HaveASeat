using haveaseat.Seeders;

namespace haveaseat.DTOs;
/// <summary>
/// Data Transfer Object (DTO) for the Cell entity.
/// </summary>
/// <remarks>
/// Used for sending full data of a cell to the client.
/// </remarks>
/// <seealso cref="Cell"/>
public class CellDTO
{
    /// <summary>
    /// Initializes a new instance of the CellDTO class.
    /// </summary>
    /// <param name="cell">The Cell entity to initialize the DTO from.</param>
    /// <seealso cref="Cell"/>
    public CellDTO(Cell cell)
    {
        Id = cell.Id;
        PositionX = cell.PositionX;
        PositionY = cell.PositionY;
        Border = cell.Border;
    }
    /// <summary>
    /// Gets or sets the unique identifier for the cell.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the X position of the cell.
    /// </summary>
    public int PositionX { get; set; }

    /// <summary>
    /// Gets or sets the Y position of the cell.
    /// </summary>
    public int PositionY { get; set; }

    /// <summary>
    /// Gets or sets the border style of the cell.
    /// </summary>
    /// <remarks>
    /// Default value is "none".
    /// </remarks>
    public string Border { get; set; } = "none";
}