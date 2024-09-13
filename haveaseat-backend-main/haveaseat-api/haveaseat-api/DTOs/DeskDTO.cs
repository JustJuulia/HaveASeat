using haveaseat.Models;

namespace haveaseat.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for the Desk entity.
/// </summary>
/// <remarks>
/// Used for sending full data of a desk to the client.
/// </remarks>
/// <seealso cref="Desk"/>
public class DeskDTO
{
    /// <summary>
    /// Initializes a new instance of the DeskDTO class.
    /// </summary>
    /// <param name="desk">The Desk entity to initialize the DTO from.</param>
    /// <seealso cref="Desk"/>
    public DeskDTO(Desk desk)
    {
        Id = desk.Id;
        PositionX = desk.PositionX;
        PositionY = desk.PositionY;
        ChairPosition = desk.ChairPosition;
    }

    /// <summary>
    /// Gets or sets the unique identifier for the desk.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the X position of the desk.
    /// </summary>
    public int PositionX { get; set; }

    /// <summary>
    /// Gets or sets the Y position of the desk.
    /// </summary>
    public int PositionY { get; set; }

    /// <summary>
    /// Gets or sets the chair position associated with the desk.
    /// </summary>
    public ChairPosition ChairPosition { get; set; }
}