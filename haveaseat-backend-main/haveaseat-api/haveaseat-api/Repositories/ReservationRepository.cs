using haveaseat.DbContexts;
using haveaseat.DTOs;
using haveaseat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace haveaseat.Repositories;

public class ReservationRepository(DataContext context) : IReservationRepository
{
    public async Task<List<ReservationDTO>> GetReservationsByUserEmail(string email)
    {
        List<Reservation> reservationsOfUser = await context.Reservations
            .Include(r => r.Desk) 
            .Where(reservation => reservation.User.Email == email)
            .ToListAsync();
        
        List<ReservationDTO> reservationDtos = reservationsOfUser.Select(r => new ReservationDTO(r)).ToList();
        
        return reservationDtos;
    }

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

    public async Task<Boolean> DeleteReservationById(long reservationId)
    {
        if (reservationId > 0)
        {
            return false;
        }
        if(await context.Reservations.Where(e=>e.Id == reservationId).ExecuteDeleteAsync() > 0){
            return true;
        }
        return false;
    }
    public async Task<List<UserDTO>> GetAllUsersFromReservationsByDate(DateOnly date)
    {

        List<User> users = await context.Reservations.Where(x => x.Date == date).Join(context.Users,  reservation => reservation.UserId, user => user.Id, ( reseevation,user) => user ).ToListAsync();
        if (users.Count == 0 || users== null)
        {
            return null;
        }
        List<UserDTO> userDTOs = users.Select(x => new UserDTO(x)).ToList();

        return userDTOs;
    }
    public async Task<List<LongTimeReservationToCheckDTQ>> longTimeReservationToCheckDTQByDeskId(long id)
    {
        List<Reservation> reservations = await context.Reservations.Where(x => x.DeskId ==id).ToListAsync();
        if (reservations.Count == 0 || reservations == null)
        {
            return null;

        }
        List<LongTimeReservationToCheckDTQ> longTimeReservationToCheckDTQs = reservations.Select(x=> new LongTimeReservationToCheckDTQ(x)).ToList();
        return longTimeReservationToCheckDTQs;
    }
}