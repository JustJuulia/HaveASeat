using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text.Json.Serialization;

namespace haveaseat.DTOs;

public class NewForbiddenDateDTO
{
     
    [JsonConstructor] public NewForbiddenDateDTO() { }    
    public NewForbiddenDateDTO(ForbiddenDate forbiddenDate)
    {
        
        Description = forbiddenDate.Description;
        Date = forbiddenDate.Date;

    }


    
    public string Description { get; set; }

    public DateOnly Date { get; set; }
}