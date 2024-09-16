namespace haveaseat.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for Resrvation entity.
/// </summary>
/// <remarks>
/// Used for sending data to client about the reservation.
/// </remarks>
/// <seealso cref="Reservation"/>
public class ReservationDTO
{
    /// <summary>
    /// Initializes a new instance of the ReservationDTO class with the specified reservation.
    /// </summary>
    /// <param name="reservation">The Reservation entity to initialize the DTO from.</param>
    /// <seealso cref="Reservation"/>
    public ReservationDTO(Reservation reservation)
    {
        Id = reservation.Id;
        Date = reservation.Date;
        Desk = new DeskDTO(reservation.Desk);
        User = new UserDTO(reservation.User);
    }
    /// <summary>
    /// Gets or sets the id of the reservation.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Gets or sets the date of the reservation.
    /// </summary>
    public DateOnly Date { get; set; }
    /// <summary>
    /// Gets or sets the DeskDTO instance of the reservation.
    /// </summary>
    public DeskDTO Desk { get; set; }
    /// <summary>
    /// Gets or sets the UserDTO instance of the reservation.
    /// </summary>
    public UserDTO User { get; set; }
}