using haveaseat.DTOs;
using haveaseat.Models;
namespace haveaseat.Repositories.Interfaces;

/// <summary>
/// This interface defines the methods for map-related operations.
/// </summary>
public interface IMapRepository
{
    /// <summary>
    /// This task retrieves all rooms.
    /// </summary>
    /// <seealso cref="RoomDTODesks"/>
    /// <returns>Returns a list of RoomDTOCells objects.</returns>
    Task<List<RoomDTOCells>> GetAllRooms();

    /// <summary>
    /// This task retrieves the entire map.
    /// </summary>
    /// <seealso cref="RoomDTO"/>
    /// <returns>Returns a list of RoomDTO objects.</returns>
    Task<List<RoomDTO>> GetAllMap();

    /// <summary>
    /// This task retrieves all desks.
    /// </summary>
    /// <seealso cref="RoomDTODesks"/>
    /// <returns>Returns a list of RoomDTODesks objects.</returns>
    Task<List<RoomDTODesks>> GetAllDesks();

    /// <summary>
    /// This task retrieves a cell by its position.
    /// </summary>
    /// <seealso cref="Cell"/>
    /// <param name="positionX">The X position of the cell.</param>
    /// <param name="positionY">The Y position of the cell.</param>
    /// <returns>Returns a Cell object.</returns>
    Task<Cell> GetCellByPosition(int positionX, int positionY);

    /// <summary>
    /// This task adds a new desk.
    /// </summary>
    /// <seealso cref="NewDeskDTO"/>
    /// <seealso cref="Cell"/>
    /// <param name="newDesk">The NewDeskDTO object containing the details of the desk to be added.</param>
    /// <param name="cell">The Cell object where the desk will be added.</param>
    /// <returns>Returns true if the desk was added, or false if the operation failed.</returns>
    Task<Boolean> AddNewDesk(NewDeskDTO newDesk, Cell cell);

    /// <summary>
    /// This task edits the chair position by desk position.
    /// </summary>
    /// <seealso cref="ChairPosition"/>
    /// <param name="positionX">The X position of the desk.</param>
    /// <param name="positionY">The Y position of the desk.</param>
    /// <param name="chairPosition">The new chair position.</param>
    /// <returns>Returns true if the chair position was edited, or false if the operation failed.</returns>
    Task<Boolean> EditChairPositionByDeskPosition(int positionX, int positionY, ChairPosition chairPosition);

    /// <summary>
    /// This task deletes a desk.
    /// </summary>
    /// <seealso cref="NewDeskDTO"/>
    /// <param name="deskDTO">The NewDeskDTO object containing the details of the desk to be deleted.</param>
    /// <returns>Returns true if the desk was deleted, or false if the operation failed.</returns>
    Task<Boolean> DeleteDesk(NewDeskDTO deskDTO);
}