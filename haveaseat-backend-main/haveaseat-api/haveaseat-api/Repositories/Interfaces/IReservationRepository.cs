using haveaseat.DTOs;

namespace haveaseat.Repositories.Interfaces;

public interface IReservationRepository
{
    Task<List<ReservationDTO>> GetReservationsByUserEmail(string email);
    Task<List<ReservationDTO>> GetReservationsByDay(DateOnly date);

    Task<NewReservationDTO> InsertReservations(NewReservationDTO reservation);
    Task<NewReservationDTO> DeleteReservationById(long reservationId);
}