using System.ComponentModel.DataAnnotations;

namespace compito_17_03.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Cognome { get; set; }

        [Required]
        public required DateOnly DataNascita { get; set; }

        [Required]
        public required string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Le password non corrispondono")]
        public required string ConfermaPassword { get; set; }
    }
}
