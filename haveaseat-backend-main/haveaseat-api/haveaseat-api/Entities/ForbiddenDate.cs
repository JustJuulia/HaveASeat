using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace haveaseat.Entities;
/// <summary>
/// This entity represents a forbbidden date with specific properties.
/// </summary>
/// <remarks>
/// This object is used for storing information about the dates when all employee have a free day for example the Independence day.
/// 
/// This class is used for creating a table in the postgresql database in which variables from this class are rows.
/// </remarks>
public class ForbiddenDate
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <remarks>
        /// The unique identifier for the forbidden date.
        /// </remarks>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <remarks>
        /// The description of the forbidden date. Maximum length is 140 characters.
        /// 
        /// There is no limit of how many dates share the same description.
        /// </remarks>
        [MaxLength(140)]
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the Date.
        /// </summary>
        /// <remarks>
        /// The date must be unique. 
        /// </remarks>
        public DateOnly Date { get; set; }

    }

