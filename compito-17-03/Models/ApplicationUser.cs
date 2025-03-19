using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace compito_17_03.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Cognome { get; set; }

        [Required]
        public DateOnly DataNascita { get; set; }
        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }
    }
}
