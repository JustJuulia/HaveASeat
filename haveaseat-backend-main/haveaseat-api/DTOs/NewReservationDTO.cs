using System.Text.Json.Serialization;

namespace haveaseat.DTOs;

public class NewReservationDTO
{
    [JsonConstructor] public NewReservationDTO() {}
    
    public NewReservationDTO(Reservation reservation)
    {
        Date = reservation.Date;
        DeskId = reservation.DeskId;
        UserId = reservation.UserId;
    }
    public DateOnly Date { get; set; }
    public long DeskId { get; set; }
    public long UserId { get; set; }
}