namespace haveaseat.DTOs;
/// <summary>
/// Data Transfer Object (DTO) for Cell entity.
/// </summary>
/// <remarks>
/// This task is used for sending data to the client about a cell and the room in which the cell is located.
/// </remarks>
/// <seealso cref="Cell"/>
/// <seealso cref="Room"/>
public class RoomDTOCells
{
    /// <summary>
    /// Initializes a new instance of the RoomDTOCells class with the specified room.
    /// </summary>
    /// <param name="room">The Room entity to initialize the DTO from.</param>
    /// <seealso cref="Room"/>
    public RoomDTOCells(Room room)
    {
        Id = room.Id;
        Name = room.Name;
        Cells = room.Cells.Select(cell => new CellDTO(cell)).ToList();
    }
    /// <summary>
    /// Gets or sets the id of the room.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the room.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the list of cells instances of the room.
    /// </summary>
    /// <seealso cref="CellDTO"/>
    public List<CellDTO> Cells { get; }
}