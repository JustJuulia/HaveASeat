using haveaseat.DTOs;

namespace haveaseat.Repositories.Interfaces;

public interface IMapRepository
{
    Task<List<RoomDTOCells>> GetAllRooms();
    Task<List<RoomDTO>> GetAllMap();
    Task<List<RoomDTODesks>> GetAllDesks();
}