using Microsoft.EntityFrameworkCore;

namespace haveaseat.Entities;
public class Area
{
    public short Id { get; set; }
    public int Height { get; set; } = 11;
    public int Width { get; set; } = 15;
}