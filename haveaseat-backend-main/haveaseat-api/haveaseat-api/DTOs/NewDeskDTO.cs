using haveaseat.Models;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Text.Json.Serialization;
namespace haveaseat.DTOs;

public class NewDeskDTO
{
    [JsonConstructor] public NewDeskDTO() { }

    public NewDeskDTO(Desk  desk) {
        
        PositionX = desk.PositionX;
        PositionY = desk.PositionY;
        ChairPosition = desk.ChairPosition;
    }
    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public ChairPosition ChairPosition { get; set; }
}

