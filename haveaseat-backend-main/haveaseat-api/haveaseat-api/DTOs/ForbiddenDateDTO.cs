using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace haveaseat.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for the ForbiddenDate entity.
/// </summary>
/// <remarks>
/// Used for sending full data of a forbidden date to the client.
/// </remarks>
/// <seealso cref="ForbiddenDate"/>
public class ForbiddenDateDTO
{
    /// <summary>
    /// Initializes a new instance of the ForbiddenDateDTO class.
    /// </summary>
    /// <seealso cref="ForbiddenDate"/>
    /// <param name="forbiddenDate">The ForbiddenDate entity to initialize the DTO from.</param>
    public ForbiddenDateDTO(ForbiddenDate forbiddenDate)
    {
        Id = forbiddenDate.Id;
        Description = forbiddenDate.Description;
        Date = forbiddenDate.Date;
    }

    /// <summary>
    /// Gets or sets the unique identifier for the forbidden date.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the description of the forbidden date.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the date of the forbidden date.
    /// </summary>
    public DateOnly Date { get; set; }
}