namespace haveaseat.DTOs;

public class RoomDTO
{
    public RoomDTO(Room room)
    {
        Id = room.Id;
        Name = room.Name;
        Cells = room.Cells.Select(cell => new CellDTO(cell)).ToList();
        Desks = room.Desks.Select(desk => new DeskDTO(desk)).ToList();
    }
    
    public long Id { get; set; }
    public string Name { get; set; }
    
    public List<CellDTO> Cells { get; }
    public List<DeskDTO> Desks { get; }
}