using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace haveaseat.DTOs;

    public class ForbiddenDateDTO
{
    public ForbiddenDateDTO(ForbiddenDate forbiddenDate)
    {
        Id = forbiddenDate.Id;
        Description = forbiddenDate.Description;
        Date = forbiddenDate.Date;
    }
   

    public long Id { get; set; }
    public string Description { get; set; }
    
    public DateOnly Date { get; set; }
}

