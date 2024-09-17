using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using haveaseat.Models;

namespace haveaseat.Entities;
/// <summary>
/// This entity represents a user with specific properties.
/// </summary>
/// <remarks>
/// This class is used for creating a table in the postgresql database where the variables from this class are rows.
/// </remarks>
public class User
{
    /// <summary>
    /// Gets or sets the Id.
    /// </summary>
    /// <remarks>
    /// The unique identifier for the user.
    /// </remarks>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    /// <summary>
    /// Gets or sets the Email.
    /// </summary>
    /// <remarks>
    /// The email of the user. Maximum length is 45 characters.
    /// 
    /// The email must be unique..
    /// </remarks>
    [MaxLength(45)]
    public string Email { get; set; }
    /// <summary>
    /// Gets or sets the Password.
    /// </summary>
    /// <remarks>
    /// The password of the user. Maximum length is 255 characters.
    /// 
    /// The password before store in database is hashed.
    /// </remarks>
    [MaxLength(255)]
    public string Password { get; set; }
    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    /// <remarks>
    /// The name of the user. Maximum length is 60 characters.
    /// 
    /// There is no need for name and surname to be unique.
    /// </remarks>
    [MaxLength(60)]

    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the Surname.
    /// </summary>
    /// <remarks>
    /// The surname of the user. Maximum length is 60 characters.
    /// 
    /// There is no need for name and surname to be unique.
    /// </remarks>
    [MaxLength(60)]
    public string Surname { get; set; }
    /// <summary>
    /// Gets or sets the salt.
    /// </summary>
    /// <remarks>
    /// The salt of the user's hashed password. Maximum length is 255 characters.
    /// 
    /// This should be accessed and displayed with extreme careful.
    /// </remarks>
    [Required]
    [MaxLength(255)]
    public string salt { get; set; }
    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    /// <seealso cref="haveaseat.Models.Role"/>
    /// <remarks>
    /// The role of the user.
    /// 
    /// The default is employee and there is no option in the api to change the role of the user.
    /// </remarks>
    public Role Role { get; set; }
    /// <summary>
    /// Gets the reservations.
    /// </summary>
    /// <seealso cref="Reservation"/>
    /// <remarks>
    /// The resrvations associated with the user.
    /// 
    /// The user can only have one reservation in the day.
    /// </remarks>
    public ICollection<Reservation> Reservations { get; }
}