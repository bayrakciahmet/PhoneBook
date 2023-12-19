using System.ComponentModel.DataAnnotations;

namespace PhoneBook.IdentityServer.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Title { get; set; }   
    }
}
