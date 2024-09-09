using haveaseat.DTOs;

namespace haveaseat.Repositories.Interfaces;
public interface IForbiddenDateRepository
{
    Task<NewForbiddenDateDTO> AddForbiddenDate(NewForbiddenDateDTO newForbiddenDate);

    Task<Boolean> DeleteForbiddenDateByDate(DateOnly date);

    Task<List<ForbiddenDateDTO>> GetAllForbiddenDates();
    Task<ForbiddenDateDTO> GetForbiddenDateByDate(DateOnly date);
    Task<ForbiddenDateDTO> GetForbiddenDateById(long id);
    Task<Boolean> EditForbiddenDateByDate(NewForbiddenDateDTO newForbiddenDate);
}

