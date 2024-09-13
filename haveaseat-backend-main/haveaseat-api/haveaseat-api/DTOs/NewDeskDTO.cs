using haveaseat.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Text.Json.Serialization;

namespace haveaseat.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for Desk entity.
/// </summary>
/// <remarks>
/// Used for getting the data for adding, editting and deleting desks from client.
/// </remarks>
/// <seealso cref="Desk"/>
public class NewDeskDTO
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NewDeskDTO"/> class.
    /// </summary>
    /// <remarks>
    /// It is needed to be able to get data from client send in JSON to the NewDeskDTO object.
    /// </remarks>
    [JsonConstructor]
    public NewDeskDTO() { }

    /// <summary>
    /// Initializes a new instance of the NewDeskDTO class with the specified desk.
    /// </summary>
    /// <seealso cref="Desk"/>
    /// <param name="desk">The Desk entity to initialize the DTO from.</param>
    public NewDeskDTO(Desk desk)
    {
        PositionX = desk.PositionX;
        PositionY = desk.PositionY;
        ChairPosition = desk.ChairPosition;
    }

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
    /// <seealso cref="ChairPosition"/>
    public ChairPosition ChairPosition { get; set; }
}

