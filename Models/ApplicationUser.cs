using Microsoft.AspNetCore.Identity;

namespace RoleBasedAuthorizationAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}