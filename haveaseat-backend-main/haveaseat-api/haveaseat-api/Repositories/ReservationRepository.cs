using haveaseat.DbContexts;
using haveaseat.DTOs;
using haveaseat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace haveaseat.Repositories;
/// <summary>
/// This repository stores reservation-related methods.
/// </summary>
/// <seealso cref="IReservationRepository"/>
/// <seealso cref="DataContext"/>
/// <param name="context">The DataContext instance used for accessing the database.</param>
public class ReservationRepository(DataContext context) : IReservationRepository
{
    /// <summary>
    /// This task retrieves reservations by user's email.
    /// </summary>
    /// <seealso cref="ReservationDTO"/>
    /// <param name="email">The email of the user whose reservations are to be retrieved.</param>
    /// <returns>Returns a list of ReservationDTO objects.</returns>
    /// <remarks>It is case insensitive.</remarks>
    public async Task<List<ReservationDTO>> GetReservationsByUserEmail(string email)
    {
        List<Reservation> reservationsOfUser = await context.Reservations
            .Include(r => r.Desk)
            .Include(r=> r.User)
            .Where(reservation => reservation.User.Email.ToLower() == email.ToLower())
            .ToListAsync();
        
        List<ReservationDTO> reservationDtos = reservationsOfUser.Select(r => new ReservationDTO(r)).ToList();
        
        return reservationDtos;
    }
    /// <summary>
    /// This task checks if a reservation on a given date and on a given desk exists.
    /// </summary>
    /// <seealso cref="Desk"/>
    /// <param name="date">The date of the reservation to check.</param>
    /// <param name="deskId">The ID of the desk to check.</param>
    /// <returns>Returns true if the reservation exists, or false if it doesn't.</returns>
    public async Task<Boolean> CheckIfReservationExistByDateAnDeskId(DateOnly date,long deskId)
    {
       
        if (await context.Reservations.Where(x => x.Date == date).Where(y => y.DeskId == deskId).SingleOrDefaultAsync() != null)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// This task retrieves reservations by date.
    /// </summary>
    /// <seealso cref="ReservationDTO"/>
    /// <param name="date">The date of the reservations to retrieve.</param>
    /// <returns>Returns a list of ReservationDTO objects.</returns>
    public async Task<List<ReservationDTO>> GetReservationsByDay(DateOnly date)
    {
        List<Reservation> reservationsOnGivenDay = await context.Reservations
            .Include(r => r.Desk)
            .Include(r => r.User)
            .Where(r => r.Date == date)
            .ToListAsync();

        List<ReservationDTO> reservationDtos = reservationsOnGivenDay
            .Select(r => new ReservationDTO(r)).ToList();

        return reservationDtos;
    }
    /// <summary>
    /// This task adds a new reservation to the database.
    /// </summary>
    /// <seealso cref="NewReservationDTO"/>
    /// <param name="reservation">The NewReservationDTO object containing the details of the reservation to be added.</param>
    /// <returns>Returns a NewReservationDTO object.</returns>
    public async Task<NewReservationDTO> InsertReservations(NewReservationDTO reservation)
    {
        Reservation entry = new Reservation
        {
            Date = reservation.Date,
            DeskId = reservation.DeskId,
            UserId = reservation.UserId
        };
        await context.Reservations.AddAsync(entry);
        await context.SaveChangesAsync();
        return reservation;
    }
    /// <summary>
    /// This task deletes a reservation with a given ID.
    /// </summary>
    /// <param name="reservationId">The ID of the reservation to be deleted.</param>
    /// <returns>Returns true if the reservation was deleted, or false if the operation failed.</returns>
    public async Task<Boolean> DeleteReservationById(long reservationId)
    {
        if (reservationId < 0)
        {
            return false;
        }

        if(await context.Reservations.Where(e=>e.Id == reservationId).ExecuteDeleteAsync() > 0){
            return true;
        }
        return false;
    }
    /// <summary>
    /// This task retrieves all users with reservations on a given date and also retrieves the ID of each reservation.
    /// </summary>
    /// <seealso cref="UserDTO"/>
    /// <param name="date">The date of the reservations to retrieve users for.</param>
    /// <returns>Returns a list of reservationUserDTOs objects if users have a reservations on given data, a null if they don't have.</returns>
    public async Task<List<ReservationUserDTO>> GetAllUsersFromReservationsByDate(DateOnly date)
    {

        List<Reservation> reservations = await context.Reservations.Where(x => x.Date == date).Include(r => r.User).ToListAsync();
        if (reservations.Count == 0 || reservations == null)
        {
            return null;
        }
        List<ReservationUserDTO> reservationUserDTOs = reservations.Select(x => new ReservationUserDTO(x)).ToList();

        return reservationUserDTOs;
    }
    /// <summary>
    /// This task retrieves reservations on given desk by Desk id.
    /// </summary>
    /// <seealso cref="LongTimeReservationToCheckDTO"/>
    /// <param name="id">The Id of the desk to retrieve reservations for.</param>
    /// <returns>Returns a list of LongTimeReservationToCheckDTO objects if Desk is booked by someone, a null if by nobody.</returns>
    public async Task<List<LongTimeReservationToCheckDTO>> longTimeReservationToCheckDTQByDeskId(long id)
    {
        List<Reservation> reservations = await context.Reservations.Include(r => r.User).Where(x => x.DeskId ==id).ToListAsync();
        if (reservations.Count == 0 || reservations == null)
        {
            return null;

        }
        List<LongTimeReservationToCheckDTO> longTimeReservationToCheckDTOs = reservations.Select(x=> new LongTimeReservationToCheckDTO(x)).ToList();
        return longTimeReservationToCheckDTOs;
      
    }
}