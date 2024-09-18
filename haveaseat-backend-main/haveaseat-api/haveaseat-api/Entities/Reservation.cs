using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace haveaseat.Entities;
/// <summary>
/// This entity represents a reservation with specific properties.
/// </summary>
/// <remarks>
/// This object is used about storing resrvation of employee.
/// 
/// This class is used for creating a table in the postgresql database in which variables from this class are rows.
/// </remarks>
public class Reservation
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    /// <remarks>
    /// The unique identifier for the cell.
    /// </remarks>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    /// <remarks>
    /// The system was designed with the assumption that employee can have only one reservation each day.
    /// </remarks>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the UserId.
    /// </summary>
    /// <remarks>
    /// The unique identifier for the User associated with the reservation.
    /// </remarks>
    public long UserId { get; set; }
    /// <summary>
    /// Gets or sets the User.
    /// </summary>
    /// <remarks>
    /// The User associated with the resrvation.
    /// </remarks>
    public User User { get; set; }
    /// <summary>
    /// Gets or sets the DeskId.
    /// </summary>
    /// <seealso cref="User"/>
    /// <remarks>
    /// The unique identifier for the Desk associated with the reservation.
    /// </remarks>
    public long DeskId { get; set; }
    /// <summary>
    /// Gets or sets the Desk.
    /// </summary>
    /// <seealso cref="Desk"/>
    /// <remarks>
    /// The Desk associated with the resrvation.
    /// 
    /// Each day only one user can have reservation on specific desk.
    /// </remarks>
    public Desk Desk { get; set; }
}