using haveaseat.DbContexts;
using haveaseat.DTOs;

using haveaseat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace haveaseat.Repositories;

public class ForbiddenDateRepository(DataContext context) : IForbiddenDateRepository
{
    public async Task<NewForbiddenDateDTO> AddForbiddenDate(NewForbiddenDateDTO newForbiddenDate)
    {
        ForbiddenDate entry = new ForbiddenDate {

            Description = newForbiddenDate.Description,
            Date = newForbiddenDate.Date

        };
        await context.ForbiddenDates.AddAsync(entry);
        await context.SaveChangesAsync();
        return newForbiddenDate;
    }
    public async Task<ForbiddenDateDTO> DeleteForbiddenDateByDate(DateOnly date)
    {
        ForbiddenDate? forbiddenDate = await context.ForbiddenDates.Where(forbiddenDate => forbiddenDate.Date == date).SingleOrDefaultAsync();
        if(forbiddenDate == null)
        {
            return null;
        }
        context.ForbiddenDates.Remove(forbiddenDate);
        await context.SaveChangesAsync();
        return new ForbiddenDateDTO(forbiddenDate);
    }
    public async Task<List<ForbiddenDateDTO>> GetAllForbiddenDates()
    {
        List<ForbiddenDate> forbiddenDates = await context.ForbiddenDates.ToListAsync();
        if (forbiddenDates == null || forbiddenDates.Count == 0)
        {

            return null;
        }
        List<ForbiddenDateDTO> forbiddenDateDTOs = forbiddenDates.Select(f => new ForbiddenDateDTO(f)).ToList();
        return forbiddenDateDTOs;

    }
    public async Task<ForbiddenDateDTO> GetForbiddenDateByDate(DateOnly date)
    {
        ForbiddenDate? forbiddenDate = await context.ForbiddenDates.Where(forbiddenDate => forbiddenDate.Date == date).SingleOrDefaultAsync();
        if (forbiddenDate == null)
        {
            return null;
        }
        ForbiddenDateDTO forbiddenDateDTO = new ForbiddenDateDTO(forbiddenDate);
        return forbiddenDateDTO;
    }
    public async Task<ForbiddenDateDTO> GetForbiddenDateById(long id)
    {
        ForbiddenDate? forbiddenDate = await context.ForbiddenDates.Where(forbiddenDate => forbiddenDate.Id == id).SingleOrDefaultAsync();
        if(forbiddenDate == null)
        {
            return null;
        }
        ForbiddenDateDTO forbiddenDateDTO= new ForbiddenDateDTO(forbiddenDate);
        return forbiddenDateDTO;
    }
    public async Task<Boolean> EditForbiddenDateByDate(DateOnly date, string description)
    {
        if (date == null)
        {
            return false;
        }
        
        if(await context.ForbiddenDates.Where(forbiddenDate => forbiddenDate.Date == date).ExecuteUpdateAsync(forbidden => forbidden.SetProperty(y => y.Description, description)) > 0)
        {
            return true;
        }
        return false;
    }
}