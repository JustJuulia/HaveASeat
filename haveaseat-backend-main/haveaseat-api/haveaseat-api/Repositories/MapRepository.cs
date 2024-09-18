using haveaseat.DbContexts;
using haveaseat.DTOs;
using haveaseat.Models;
using haveaseat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace haveaseat.Repositories;
/// <summary>
/// This repository stores map-related methods.
/// </summary>
/// <seealso cref="IMapRepository"/>
/// <seealso cref="DataContext"/>
/// <param name="context">The DataContext instance used for accessing the database.</param>
public class MapRepository(DataContext context) : IMapRepository
{
    /// <summary>
    /// This task retrieves all desks on the map.
    /// </summary>
    /// <seealso cref="RoomDTODesks"/>
    /// <returns>Returns a list of RoomDTODesks objects.</returns>
    public async Task<List<RoomDTODesks>> GetAllDesks()
    {
        List<Room> rooms = await context.Rooms
            .Include(r => r.Desks)
            .ToListAsync();
        List<RoomDTODesks> deskDtos = rooms.Select(d => new RoomDTODesks(d)).ToList();
        return deskDtos;
    }
    /// <summary>
    /// This task retrieves all rooms on the map.
    /// </summary>
    /// <seealso cref="RoomDTOCells"/>
    /// <returns>Returns a list of RoomDTOCells objects.</returns>
    public async Task<List<RoomDTOCells>> GetAllRooms()
    {
        List<Room> rooms = await context.Rooms
            .Include(c => c.Cells)
            .ToListAsync();

        List<RoomDTOCells> roomDtos = rooms.Select(room => new RoomDTOCells(room)).ToList();
        return roomDtos;
    }
    /// <summary>
    /// This task retrieves the entire map.
    /// </summary>
    /// <seealso cref="RoomDTO"/>
    /// <returns>Returns a list of RoomDTO objects.</returns>
    public async Task<List<RoomDTO>> GetAllMap()
    {
        List<Room> rooms = await context.Rooms
            .Include(r => r.Cells)
            .Include(r => r.Desks)
            .ToListAsync();

        List<RoomDTO> roomDtos = rooms.Select(room => new RoomDTO(room)).ToList();
        return roomDtos;
    }
    /// <summary>
    /// This task retrieves a cell by its position.
    /// </summary>
    /// <seealso cref="Cell"/>
    /// <param name="positionX">The X position of the cell.</param>
    /// <param name="positionY">The Y position of the cell.</param>
    /// <returns>Returns the Cell object if found, or null if not found.</returns>
    public async Task<Cell> GetCellByPosition(int positionX, int positionY)
    {
        Cell? cell= await context.Cells.Where(x=> x.PositionX == positionX).Where(y => y.PositionY ==positionY).SingleOrDefaultAsync();
        return cell;
    }
    /// <summary>
    /// This task adds a new desk.
    /// </summary>
    /// <seealso cref="NewDeskDTO"/>
    /// <seealso cref="Cell"/>
    /// <remarks>
    /// Cell object is used to get information about in what room will be new desk.
    /// </remarks>
    /// <param name="newDesk">The NewDeskDTO object containing the desk's details.</param>
    /// <param name="cell">The Cell object which contains information about where the desk will be added.</param>
    /// <returns>Returns true if the desk was added successfully, or false if the position is already occupied.</returns>
    public async Task<Boolean> AddNewDesk(NewDeskDTO newDesk, Cell cell)
    {
        Desk? deskCheck = await context.Desks.Where(x=> x.PositionX == cell.PositionX).Where(y=>y.PositionY == cell.PositionY).SingleOrDefaultAsync();
        if (deskCheck != null)
        {
            return false;
        }
        
        Desk desk= new Desk
        {
            PositionX = newDesk.PositionX,
            PositionY = newDesk.PositionY,
            ChairPosition = newDesk.ChairPosition,
            RoomId = cell.RoomId,
            Room = cell.Room
        };
        await context.Desks.AddAsync(desk);
        
        await context.SaveChangesAsync();

        return true;
    }
    /// <summary>
    /// This task edits the chair position by desk position.
    /// </summary>
    /// <seealso cref="ChairPosition"/>
    /// <param name="positionX">The X position of the desk.</param>
    /// <param name="positionY">The Y position of the desk.</param>
    /// <param name="chairPosition">The new chair position.</param>
    /// <returns>Returns true if the operation was successful, or false if it failed.</returns>
    public async Task<Boolean> EditChairPositionByDeskPosition(int positionX, int positionY, ChairPosition chairPosition)
    {
        if(!(chairPosition == ChairPosition.TOP || chairPosition == ChairPosition.RIGHT || chairPosition== ChairPosition.LEFT || chairPosition == ChairPosition.BOTTOM))
        {
            return false;
        }
        if(await context.Desks.Where(desk => desk.PositionX==positionX).Where(desk=>desk.PositionY==positionY).ExecuteUpdateAsync(desk => desk.SetProperty(e => e.ChairPosition, chairPosition)) > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// This task deletes a desk.
    /// </summary>
    /// <seealso cref="NewDeskDTO"/>
    /// <param name="deskDTO">The NewDeskDTO object containing the desk's details.</param>
    /// <returns>Returns true if the operation was successful, or false if it failed.</returns>
    public async Task<Boolean> DeleteDesk(NewDeskDTO deskDTO)
    {
        
        long id =(await context.Desks.Where(desk => desk.PositionX == deskDTO.PositionX).Where(desk => desk.PositionY == deskDTO.PositionY).SingleOrDefaultAsync()).Id;
        if (id < 0)
        {
            return false;
        }
        await context.Desks.Where(e=>e.Id==id).Join(context.Reservations, x=>x.Id, y=>y.DeskId,(x,y)=>y).ExecuteDeleteAsync();
        if(await context.Desks.Where(x => x.Id == id).ExecuteDeleteAsync() > 0)
        {
            return true;
        }
        return false; 
        

    }
}