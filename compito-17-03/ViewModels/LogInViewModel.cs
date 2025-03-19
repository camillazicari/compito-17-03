using System.ComponentModel.DataAnnotations;

namespace compito_17_03.ViewModels
{
    public class LogInViewModel
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
