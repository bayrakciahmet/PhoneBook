using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Web.Models
{
    public class SigninInput
    {
        [Required(ErrorMessage = "Email Alanı Gereklidir")]
        [Display(Name = "Email adresiniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre Alanı Gereklidir")]
        [Display(Name = "Şifreniz")]
        public string Password { get; set; }

        [Display(Name = "Beni hatırla")]
        public bool IsRemember { get; set; }
    }
}
