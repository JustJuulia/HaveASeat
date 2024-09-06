using haveaseat.DbContexts;
using haveaseat.DTOs;
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
}