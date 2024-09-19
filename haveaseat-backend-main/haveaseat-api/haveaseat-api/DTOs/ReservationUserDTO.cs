using haveaseat.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Xml.Linq;

namespace haveaseat.DTOs;
    /// <summary>
    /// Data Transfer Object (DTO) for User entity.
    /// </summary>
    /// <seealso cref="User"/>
    /// <seealso cref="Reservation"/>
    /// <remarks>
    /// Used for sending data about all user which has reservation on given date with reservation id.
    /// </remarks>

    public class ReservationUserDTO
    {

    /// <summary>
    /// Initializes a new instance of the ResrvationUserDTO class with the specified reservation.
    /// </summary>
    /// <param name="reservation">The Reservation entity to initialize the DTO from.</param>
    /// <seealso cref="Reservation"/>
    public ReservationUserDTO(Reservation reservation)
        {
            Id = reservation.UserId;
            Email = reservation.User.Email;
            Name = reservation.User.Name;
            Surname = reservation.User.Surname;
            Role = reservation.User.Role;
            ReservationId = reservation.Id;
        }

    /// <summary>
    /// Gets or sets the id of the user.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the surname of the user.
    /// </summary>
    public string Surname { get; set; }
    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public Role Role { get; set; }
    /// <summary>
    /// Gets or sets the id of the reservation associated with the user.
    /// </summary>
    public long ReservationId { get; set; }
}

