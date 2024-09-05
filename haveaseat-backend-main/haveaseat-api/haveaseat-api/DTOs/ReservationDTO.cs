namespace haveaseat.DTOs;

public class ReservationDTO
{
    public ReservationDTO(Reservation reservation)
    {
        Id = reservation.Id;
        Date = reservation.Date;
        Desk = new DeskDTO(reservation.Desk);
        User = new UserDTO(reservation.User);
    }
    public long Id { get; set; }
    public DateOnly Date { get; set; }
    public DeskDTO Desk { get; set; }
    public UserDTO User { get; set; }
}