using Microsoft.AspNetCore.Identity;

namespace UserManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ServiceUserName { get; set; } = string.Empty;
        public string ServicePassword { get; set; } = string.Empty;
    }
}
