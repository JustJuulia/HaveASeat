namespace haveaseat.DTOs;
/// <summary>
/// Data Transfer Object (DTO) for Room entity.
/// </summary>
/// <remarks>
/// Used for sending data to client about the room.
/// </remarks>
/// <seealso cref="Room"/>
public class RoomDTO
{
    /// <summary>
    /// Initializes a new instance of the RoomDTO class with the specified room.
    /// </summary>
    /// <param name="room">The Room entity to initialize the DTO from.</param>
    /// <seealso cref="Room"/>
    public RoomDTO(Room room)
    {
        Id = room.Id;
        Name = room.Name;
        Cells = room.Cells.Select(cell => new CellDTO(cell)).ToList();
        Desks = room.Desks.Select(desk => new DeskDTO(desk)).ToList();
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
    public List<CellDTO> Cells { get; }
    /// <summary>
    /// Gets or sets the list of desks instances of the room.
    /// </summary>
    public List<DeskDTO> Desks { get; }
}