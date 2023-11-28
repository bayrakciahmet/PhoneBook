using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Web.Models.Reports
{
    public class ReportUpdateInput
    {
        public int Id { get; set; }

        [Display(Name ="Rapor Adı")]
        public string ReportName { get; set; }

        [Display(Name ="Rapor Durumu")]
        public string? Status { get; set; }

        public List<Models.Reports.ReportLocationViewModel>? reportLocations { get; set; }

    }
}
