using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace haveaseat.Entities;
/// <summary>
/// This entity represents a room with specific properties.
/// </summary>
/// <remarks>
/// The whole map is divided into rooms. Room stores the information about the cells and desks in it.
/// 
/// This class is used for creating a table in the postgresql database in which variables from this class are rows.
/// </remarks>
public class Room
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    /// <remarks>
    /// The unique identifier for the room.
    /// </remarks>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    /// <remarks>
    /// The name of room. Maximum length is 4 characters.
    /// </remarks>
    [MaxLength(4)]
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the desks in the room.
    /// </summary>
    /// <seealso cref="Desk"/>
    public ICollection<Desk>? Desks { get; }
    /// <summary>
    /// Gets or sets the cells in the room.
    /// </summary>
    /// <seealso cref="Cell"/>
    /// <remarks>
    /// Cells can't exist outside the room.
    /// </remarks>
    public ICollection<Cell> Cells { get; }
}
