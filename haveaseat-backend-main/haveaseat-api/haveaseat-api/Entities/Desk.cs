using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using haveaseat.Models;

namespace haveaseat.Entities;
/// <summary>
/// This entity represents a desk with specific properties.
/// </summary>
/// <remarks>
/// These objects represent desks thats are boooked by employee of the office.
/// 
/// All desk share position with the cells but not all cells share position with desks.
/// 
/// This class is used for creating a table in the postgresql database in which variables from this class are rows.
/// </remarks>
public class Desk
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    /// <remarks>
    /// The unique identifier for the desk.
    /// </remarks>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]

    public long Id { get; set; }
    /// <summary>
    /// Gets or sets the X position of the cell.
    /// </summary>
    public int PositionX { get; set; }
    /// <summary>
    /// Gets or sets the Y position of the cell.
    /// </summary>
    public int PositionY { get; set; }
    /// <summary>
    /// Gets or sets the chair position of the desk.
    /// </summary>
    /// <seealso cref="haveaseat.Models.ChairPosition"/>
    /// <remarks>
    /// This property is used for displaying desk where desk rotation is equal chair position. Desks without chairposition cannot be displayed.
    /// </remarks>
    public ChairPosition ChairPosition { get; set; }
    /// <summary>
    /// Gets or sets the resrvations of the user.
    /// </summary>
    /// <seealso cref="Reservation"/>
    /// <remarks>
    /// When the desk is deleted then all reservations on that desk too.
    /// </remarks>
    public ICollection<Reservation> Reservations { get; }
    /// <summary>
    /// Gets or sets the RoomId.
    /// </summary>
    /// <remarks>
    /// The unique identifier for the room associated with the desk.
    /// </remarks>
    public long RoomId { get; set; }

    /// <summary>
    /// Gets or sets the Room.
    /// </summary>
    /// <seealso cref="Room"/>
    /// <remarks>
    /// The room associated with the desk.
    /// </remarks>
    public Room Room { get; set; }

}