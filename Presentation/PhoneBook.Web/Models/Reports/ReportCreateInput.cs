using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Web.Models.Reports
{
    public class ReportCreateInput
    {
        [Display(Name ="Rapor Adı")]
        public string ReportName { get; set; }

    }
}
