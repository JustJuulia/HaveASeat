using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Text.Json.Serialization;

namespace haveaseat.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for ForbiddenDate entity.
/// </summary>
/// <remarks>
/// Used for getting data from client for adding, editing and deleting forbidden dates.
/// </remarks>
/// <seealso cref="ForbiddenDate"/>
public class NewForbiddenDateDTO
{
    /// <summary>
    /// Initializes a new instance of the NewForbiddenDateDTO class.
    /// </summary>
    /// <remarks>
    /// It is needed to be able to get data from client sent in JSON to the NewForbiddenDateDTO object.
    /// </remarks>
    [JsonConstructor]
    public NewForbiddenDateDTO() { }

    /// <summary>
    /// Initializes a new instance of the NewForbiddenDateDTO class with the specified forbidden date.
    /// </summary>
    /// <seealso cref="ForbiddenDate"/>
    /// <param name="forbiddenDate">The ForbiddenDate entity to initialize the DTO from.</param>
    public NewForbiddenDateDTO(ForbiddenDate forbiddenDate)
    {
        Description = forbiddenDate.Description;
        Date = forbiddenDate.Date;
    }

    /// <summary>
    /// Gets or sets the description of the forbidden date.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the date of the forbidden date.
    /// </summary>
    public DateOnly Date { get; set; }
}