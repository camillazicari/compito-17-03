using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace compito_17_03.Models
{
    [Table("Studenti")]
    public class Studente
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Cognome { get; set; }

        [Required]
        public DateOnly DataNascita { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
