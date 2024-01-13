using Microsoft.AspNetCore.Identity;

namespace Sklep_internetowy_projekt.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}

