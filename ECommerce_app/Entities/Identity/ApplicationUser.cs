using Microsoft.AspNetCore.Identity;

namespace ECommerce_app.Entities.User
{
    public class ApplicationUser: IdentityUser
    {
        public string Gender { get; set; }  
        public string DisplayName { get; set; } 
    }
}
