using Microsoft.AspNetCore.Identity;

namespace PhoneBook.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Title { get; set; }   
    }
}
