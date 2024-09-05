using haveaseat_api.Seeders;

namespace haveaseat.DTOs;

public class CellDTO
{
    public CellDTO(Cell cell)
    {
        Id = cell.Id;
        PositionX = cell.PositionX;
        PositionY = cell.PositionY;
        Border = cell.Border;
    }
    
    public long Id { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public string Border { get; set; } = "none";
}