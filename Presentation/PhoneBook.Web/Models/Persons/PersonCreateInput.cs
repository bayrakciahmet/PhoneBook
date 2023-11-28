
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Web.Models.Persons
{
    public class PersonCreateInput
    {

        [Required]
        [Display(Name ="Ad")]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Soyad")]
        public string? LastName { get; set; }

        [Display(Name = "Firma")]
        public string? Company { get; set; }

    }
}
