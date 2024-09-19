using haveaseat.DbContexts;
using haveaseat.DTOs;

using haveaseat.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace haveaseat.Repositories;
/// <summary>
/// This repository stores forbidden dates-related methods.
/// </summary>
/// <seealso cref="DataContext"/>
/// <seealso cref="IForbiddenDateRepository"/>
/// <param name="context">The DataContext instance used for accessing the database.</param>
public class ForbiddenDateRepository(DataContext context) : IForbiddenDateRepository
{
    /// <summary>
    /// This task adds a new forbidden date.
    /// </summary>
    /// <seealso cref="NewForbiddenDateDTO"/>
    /// <param name="newForbiddenDate">The NewForbiddenDateDTO object containing the forbidden date's details.</param>
    /// <returns>Returns the NewForbiddenDateDTO object of the added forbidden date if operation was succesfuly or a null if operation failed.</returns>
    public async Task<NewForbiddenDateDTO> AddForbiddenDate(NewForbiddenDateDTO newForbiddenDate)
    {
        ForbiddenDate entry = new ForbiddenDate {

            Description = newForbiddenDate.Description,
            Date = newForbiddenDate.Date

        };
        await context.ForbiddenDates.AddAsync(entry);
        int result = await context.SaveChangesAsync();
        if (result > 0)
        {
            return newForbiddenDate;
        }
        return null;
    }
    /// <summary>
    /// This task deletes a forbidden date by its date.
    /// </summary>
    /// <param name="date">The date of the forbidden date to be deleted.</param>
    /// <returns>Returns true if the deletion was successful, or false if it failed.</returns>
    public async Task<Boolean> DeleteForbiddenDateByDate(DateOnly date)
    {
        if (await context.ForbiddenDates.Where(forbiddenDate => forbiddenDate.Date == date).ExecuteDeleteAsync() > 0)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// This task retrieves all forbidden dates.
    /// </summary>
    /// <seealso cref="ForbiddenDateDTO"/>
    /// <returns>Returns a list of ForbiddenDateDTO objects, or null if no forbidden dates are found.</returns>
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
    /// <summary>
    /// This task retrieves a forbidden date by its date.
    /// </summary>
    /// <seealso cref="ForbiddenDateDTO"/>
    /// <param name="date">The date of the forbidden date to be retrieved.</param>
    /// <returns>Returns a ForbiddenDateDTO object if the forbidden date is found, or null if it is not found.</returns>
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
    /// <summary>
    /// This task retrieves a forbidden date by its ID.
    /// </summary>
    /// <seealso cref="ForbiddenDateDTO"/>
    /// <param name="id">The ID of the forbidden date to be retrieved.</param>
    /// <returns>Returns a ForbiddenDateDTO object if the forbidden date is found, or null if it is not found.</returns>
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
    /// <summary>
    /// This task edits a forbidden date by its date.
    /// </summary>
    /// <seealso cref="NewForbiddenDateDTO"/>
    /// <param name="newForbiddenDate">The NewForbiddenDateDTO object containing the updated details of the forbidden date.</param>
    /// <returns>Returns true if the update was successful, or false if it failed.</returns>
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