using System.Text.Json.Serialization;

namespace haveaseat.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for Reservation entity.
/// </summary>
/// <remarks>
/// Used for getting data from client for adding, deleting Reservations.
/// </remarks>
/// <seealso cref="Reservation"/>
public class NewReservationDTO
{
    /// <summary>
    /// Initializes a new instance of the NewReservationDTO class.
    /// </summary>
    /// <remarks>
    /// It is needed to be able to get data from client sent in JSON to the NewReservationDTO object.
    /// </remarks>
    [JsonConstructor]
    public NewReservationDTO() { }

    /// <summary>
    /// Initializes a new instance of the NewReservationDTO class with the specified reservation.
    /// </summary>
    /// <param name="reservation">The Reservation entity to initialize the DTO from.</param>
    public NewReservationDTO(Reservation reservation)
    {
        Date = reservation.Date;
        DeskId = reservation.DeskId;
        UserId = reservation.UserId;
    }

    /// <summary>
    /// Gets or sets the date of the reservation.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the ID of the desk associated with the reservation.
    /// </summary>
    public long DeskId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user associated with the reservation.
    /// </summary>
    public long UserId { get; set; }
}