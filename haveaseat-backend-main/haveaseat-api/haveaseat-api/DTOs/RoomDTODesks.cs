namespace haveaseat.DTOs;
/// <summary>
/// Data Transfer Object (DTO) for Desk entity.
/// </summary>
/// <remarks>
/// This task is used for sending data to the client about a desk and the room in which the cell is located.
/// </remarks>
/// <seealso cref="Desk"/>
/// <seealso cref="Room"/>
public class RoomDTODesks
{
    /// <summary>
    /// Initializes a new instance of the RoomDTODesks class with the specified room.
    /// </summary>
    /// <param name="room">The Room entity to initialize the DTO from.</param>
    /// <seealso cref="Room"/>
    public RoomDTODesks(Room room)
    {
        Id = room.Id;
        Name = room.Name;
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
    /// Gets or sets the list of desks instances of the room.
    /// </summary>
    /// <seealso cref="DeskDTO"/>
    public List<DeskDTO> Desks { get; }
}