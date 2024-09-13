using haveaseat.DTOs;

namespace haveaseat.Repositories.Interfaces;

public interface IReservationRepository
{
    Task<List<ReservationDTO>> GetReservationsByUserEmail(string email);
    Task<List<ReservationDTO>> GetReservationsByDay(DateOnly date);
    Task<Boolean> CheckIfReservationExistByDateAnDeskId(DateOnly date, long deskId);
    Task<NewReservationDTO> InsertReservations(NewReservationDTO reservation);
    Task<Boolean> DeleteReservationById(long reservationId);
    Task<List<UserDTO>> GetAllUsersFromReservationsByDate(DateOnly date);
    Task<List<LongTimeReservationToCheckDTO>> longTimeReservationToCheckDTQByDeskId(long id);
}