using Microsoft.AspNetCore.Identity;

namespace compito_17_03.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }

    }
}
