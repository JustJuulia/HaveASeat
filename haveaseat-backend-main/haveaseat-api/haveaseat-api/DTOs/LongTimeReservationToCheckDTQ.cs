using System.Text.Json.Serialization;

namespace haveaseat.DTOs;
/// <summary>
/// Data Transfer Object (DTO) for reservations.
/// </summary>
/// <remarks>
/// Used for sending user data who has reservation on the desk with given id. It is send to the client in the list.
/// </remarks>
/// <seealso cref="Reservation"/>
public class LongTimeReservationToCheckDTO
{


    /// <summary>
    /// Initializes a new instance of the LongTimeReservationToCheckDTO class with the specified reservation.
    /// </summary>
    /// <param name="reservation">The Reservation entity to initialize the DTO from.</param>
    /// <seealso cref="Reservation"/>
    public LongTimeReservationToCheckDTO(Reservation reservation)
    {
        Date = reservation.Date;
        User = new UserDTO(reservation.User);
    }

    /// <summary>
    /// Gets or sets the date of the reservation.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the user associated with the reservation.
    /// </summary>
    public UserDTO User { get; set; }
}