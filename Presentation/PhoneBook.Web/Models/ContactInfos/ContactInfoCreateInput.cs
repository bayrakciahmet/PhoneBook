using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Web.Models.ContactInfos
{
    public class ContactInfoCreateInput
    {
        [Required(ErrorMessage = "Kişi Id Alanı Gereklidir")]
        public string PersonId { get; set; }

        [Display(Name = "Bilgi Tipi")]
        [Required(ErrorMessage = "Bilgi Tipi Alanı Gereklidir")]
        public string InfoType { get; set; }


        [Display(Name = "Bilgi İçeriği")]
        [Required(ErrorMessage = "İçerik Alanı Gereklidir")]
        public string InfoContent { get; set; }


        public string? FullName { get; set; }

    }
}
