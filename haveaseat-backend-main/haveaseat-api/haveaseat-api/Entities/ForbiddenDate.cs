using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace haveaseat.Entities;

    public class ForbiddenDate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [MaxLength(140)]
        public string Description { get; set; }
        public DateOnly Date { get; set; }

    }

