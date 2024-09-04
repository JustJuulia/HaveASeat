using haveaseat.Models;

namespace haveaseat.DTOs;

public class DeskDTO
{
    public DeskDTO(Desk desk)
    {
        Id = desk.Id;
        PositionX = desk.PositionX;
        PositionY = desk.PositionY;
        ChairPosition = desk.ChairPosition;
    }
    public long Id { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    
    public ChairPosition ChairPosition { get; set; }
}