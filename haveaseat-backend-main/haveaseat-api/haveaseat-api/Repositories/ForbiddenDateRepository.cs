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
    public async Task<Boolean> DeleteForbiddenDateByDate(DateOnly date)
    {
        if (await context.ForbiddenDates.Where(forbiddenDate => forbiddenDate.Date == date).ExecuteDeleteAsync() > 0)
        {
            return true;
        }
        return false;
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
    public async Task<Boolean> EditForbiddenDateByDate(NewForbiddenDateDTO newForbiddenDate)
    {
        
        if (newForbiddenDate.Date == null)
        {
            return false;
        }
        
        if(await context.ForbiddenDates.Where(forbiddenDate => forbiddenDate.Date == newForbiddenDate.Date).ExecuteUpdateAsync(forbidden => forbidden.SetProperty(y => y.Description, newForbiddenDate.Description)) > 0)
        {
            return true;
        }
        return false;
    }
}