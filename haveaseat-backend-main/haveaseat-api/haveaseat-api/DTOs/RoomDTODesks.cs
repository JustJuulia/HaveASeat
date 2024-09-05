namespace haveaseat.DTOs;

public class RoomDTODesks
{
    public RoomDTODesks(Room room)
    {
        Id = room.Id;
        Name = room.Name;
        Desks = room.Desks.Select(desk => new DeskDTO(desk)).ToList();
    }
    
    public long Id { get; set; }
    public string Name { get; set; }
    public List<DeskDTO> Desks { get; }
}