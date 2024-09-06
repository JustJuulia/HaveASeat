using System.Text.Json.Serialization;

namespace haveaseat.DTOs;
public class LongTimeReservationToCheckDTQ
{
    [JsonConstructor] public LongTimeReservationToCheckDTQ() { }
    public LongTimeReservationToCheckDTQ(Reservation reservation)
    {

        Date = reservation.Date;
        User = new UserDTO(reservation.User);
    }
    public DateOnly Date { get; set; }
    public UserDTO User { get; set; }
}
