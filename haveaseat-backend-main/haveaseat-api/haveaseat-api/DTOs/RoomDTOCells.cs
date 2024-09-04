namespace haveaseat.DTOs;

public class RoomDTOCells
{
    public RoomDTOCells(Room room)
    {
        Id = room.Id;
        Name = room.Name;
        Cells = room.Cells.Select(cell => new CellDTO(cell)).ToList();
    }
    
    public long Id { get; set; }
    public string Name { get; set; }
    
    public List<CellDTO> Cells { get; }
}