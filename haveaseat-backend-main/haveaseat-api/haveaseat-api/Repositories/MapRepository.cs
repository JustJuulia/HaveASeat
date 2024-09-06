using haveaseat.DbContexts;
using haveaseat.DTOs;
using haveaseat.Models;
using haveaseat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace haveaseat.Repositories;

public class MapRepository(DataContext context) : IMapRepository
{
    public async Task<List<RoomDTODesks>> GetAllDesks()
    {
        List<Room> rooms = await context.Rooms
            .Include(r => r.Desks)
            .ToListAsync();
        List<RoomDTODesks> deskDtos = rooms.Select(d => new RoomDTODesks(d)).ToList();
        return deskDtos;
    }
    public async Task<List<RoomDTOCells>> GetAllRooms()
    {
        List<Room> rooms = await context.Rooms
            .Include(c => c.Cells)
            .ToListAsync();

        List<RoomDTOCells> roomDtos = rooms.Select(room => new RoomDTOCells(room)).ToList();
        return roomDtos;
    }

    public async Task<List<RoomDTO>> GetAllMap()
    {
        List<Room> rooms = await context.Rooms
            .Include(r => r.Cells)
            .Include(r => r.Desks)
            .ToListAsync();

        List<RoomDTO> roomDtos = rooms.Select(room => new RoomDTO(room)).ToList();
        return roomDtos;
    }
    public async Task<Cell> GetCellByPosition(int positionX, int positionY)
    {
        Cell cell= await context.Cells.Where(x=> x.PositionX == positionX).Where(x => x.PositionY ==positionY).SingleOrDefaultAsync();
        if (cell == null)
        {

            return null;

        }
        return cell;
    }
    public async Task<Boolean> AddNewDesk(NewDeskDTO newDesk, Cell cell)
    {
        Desk deskCheck = await context.Desks.Where(x=> x.PositionX == cell.PositionX).Where(x=>x.PositionX == cell.PositionX).SingleOrDefaultAsync();
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
}