using haveaseat.DTOs;
using haveaseat.Models;
namespace haveaseat.Repositories.Interfaces;

public interface IMapRepository
{
    Task<List<RoomDTOCells>> GetAllRooms();
    Task<List<RoomDTO>> GetAllMap();
    Task<List<RoomDTODesks>> GetAllDesks();
    Task<Cell> GetCellByPosition(int positionX, int positionY);
    Task<Boolean> AddNewDesk(NewDeskDTO newDesk, Cell cell);

    Task<Boolean> EditChairPositionByDeskPosition(int positonX, int positonY, ChairPosition chairPosition);
    

}