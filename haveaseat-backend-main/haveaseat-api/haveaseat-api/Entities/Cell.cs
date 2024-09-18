using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace haveaseat.Entities;
/// <summary>
/// This entity represents a cell with specific properties.
/// </summary>
/// <seealso cref="Room"/>
/// <remarks>
/// The cells are used for deawing the office walls. These objects are occupaying all the map's space.
/// 
/// This class is used for creating a table in the postgresql database in which variables from this class are rows.
/// </remarks>
public class Cell
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
    /// Gets or sets the X position of the cell.
    /// </summary>
    public int PositionX { get; set; }
    /// <summary>
    /// Gets or sets the Y position of the cell.
    /// </summary>
    public int PositionY { get; set; }
    /// <summary>
    /// Gets or sets the Border.
    /// </summary>
    /// <remarks>
    /// The border style of the cell. Default value is "none". Maximum length is 50 characters.
    /// </remarks>
    [MaxLength(50)]
    public string Border { get; set; } = "none";
    /// <summary>
    /// Gets or sets the Room.
    /// </summary>
    /// <remarks>
    /// The room associated with the cell.
    /// </remarks>
    public Room Room { get; set; }
    /// <summary>
    /// Gets or sets the RoomId.
    /// </summary>
    /// <remarks>
    /// The unique identifier for the room associated with the cell.
    /// </remarks>
    public long RoomId { get; set; }
}