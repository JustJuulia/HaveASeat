using haveaseat.DTOs;

namespace haveaseat.Repositories.Interfaces;


/// <summary>
/// This interface defines the methods for reservation-related operations.
/// <seealso cref="haveaseat.Repositories.ReservationRepository"/>
/// </summary>

public interface IReservationRepository
{
    /// <summary>
    /// This task retrieves reservations by user's email.
    /// </summary>
    /// <seealso cref="ReservationDTO"/>
    /// <param name="email">The email of the user whose reservations are to be retrieved.</param>
    /// <returns>Returns a list of ReservationDTO objects.</returns>
    Task<List<ReservationDTO>> GetReservationsByUserEmail(string email);
    /// <summary>
    /// This task retrieves reservations by date.
    /// </summary>
    /// <seealso cref="ReservationDTO"/>
    /// <param name="date">The date of the reservations to retrieve.</param>
    /// <returns>Returns a list of ReservationDTO objects.</returns>
    Task<List<ReservationDTO>> GetReservationsByDay(DateOnly date);
    /// <summary>
    /// This task checks if a reservation on a given date and on a given desk exists.
    /// </summary>
    /// <seealso cref="Desk"/>
    /// <param name="date">The date of the reservation to check.</param>
    /// <param name="deskId">The ID of the desk to check.</param>
    /// <returns>Returns true if the reservation exists, or false if it doesn't.</returns>
    Task<Boolean> CheckIfReservationExistByDateAnDeskId(DateOnly date, long deskId);
    /// <summary>
    /// This task adds a new reservation to the database.
    /// </summary>
    /// <seealso cref="NewReservationDTO"/>
    /// <param name="reservation">The NewReservationDTO object containing the details of the reservation to be added.</param>
    /// <returns>Returns a NewReservationDTO object.</returns>
    Task<NewReservationDTO> InsertReservations(NewReservationDTO reservation);
    /// <summary>
    /// This task deletes a reservation with a given ID.
    /// </summary>
    /// <param name="reservationId">The ID of the reservation to be deleted.</param>
    /// <returns>Returns true if the reservation was deleted, or false if the operation failed.</returns>
    Task<Boolean> DeleteReservationById(long reservationId);
    /// <summary>
    /// This task retrieves all users with reservations on a given date and also retrieves the ID of each reservation.
    /// </summary>
    /// <seealso cref="ReservationUserDTO"/>
    /// <param name="date">The date of the reservations to retrieve users for.</param>
    /// <returns>Returns a list of ReservationUserDTO objects.</returns>
    Task<List<ReservationUserDTO>> GetAllUsersFromReservationsByDate(DateOnly date);
    /// <summary>
    /// This task retrieves all reservations on desk by desk ID.
    /// </summary>
    /// <seealso cref="Desk"/>
    /// <param name="id">The ID of the desk to retrieve reservations for.</param>
    /// <returns>Returns a list of LongTimeReservationToCheckDTO objects.</returns>
    Task<List<LongTimeReservationToCheckDTO>> longTimeReservationToCheckDTQByDeskId(long id);
}