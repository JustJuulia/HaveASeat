using haveaseat.DTOs;

namespace haveaseat.Repositories.Interfaces;
/// <summary>
/// This interface defines the methods for forbidden date-related operations.
/// </summary>
/// <seealso cref="haveaseat.Repositories.ForbiddenDateRepository"/>
public interface IForbiddenDateRepository
{
    /// <summary>
    /// This task adds a new forbidden date.
    /// </summary>
    /// <seealso cref="NewForbiddenDateDTO"/>
    /// <param name="newForbiddenDate">The NewForbiddenDateDTO object containing the details of the forbidden date to be added.</param>
    /// <returns>Returns a NewForbiddenDateDTO object.</returns>
    Task<NewForbiddenDateDTO> AddForbiddenDate(NewForbiddenDateDTO newForbiddenDate);

    /// <summary>
    /// This task deletes a forbidden date by date.
    /// </summary>
    /// <param name="date">The date of the forbidden date to be deleted.</param>
    /// <returns>Returns true if the forbidden date was deleted, or false if the operation failed.</returns>
    Task<Boolean> DeleteForbiddenDateByDate(DateOnly date);
    /// <summary>
    /// This task retrieves all forbidden dates.
    /// </summary>
    /// <seealso cref="ForbiddenDateDTO"/>
    /// <returns>Returns a list of ForbiddenDateDTO objects.</returns
    Task<List<ForbiddenDateDTO>> GetAllForbiddenDates();
    /// <summary>
    /// This task retrieves a forbidden date by date.
    /// </summary>
    /// <seealso cref="ForbiddenDateDTO"/>
    /// <param name="date">The date of the forbidden date to retrieve.</param>
    /// <returns>Returns a ForbiddenDateDTO object.</returns>
    Task<ForbiddenDateDTO> GetForbiddenDateByDate(DateOnly date);
    /// <summary>
    /// This task retrieves a forbidden date by ID.
    /// </summary>
    /// <seealso cref="ForbiddenDateDTO"/>
    /// <param name="id">The ID of the forbidden date to retrieve.</param>
    /// <returns>Returns a ForbiddenDateDTO object.</returns>
    Task<ForbiddenDateDTO> GetForbiddenDateById(long id);
    /// <summary>
    /// This task edits a forbidden date by date.
    /// </summary>
    /// <seealso cref="NewForbiddenDateDTO"/>
    /// <param name="newForbiddenDate">The NewForbiddenDateDTO object containing the updated details of the forbidden date.</param>
    /// <returns>Returns true if the forbidden date was edited, or false if the operation failed.</returns>
    Task<Boolean> EditForbiddenDateByDate(NewForbiddenDateDTO newForbiddenDate);
}

