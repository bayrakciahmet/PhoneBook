namespace PhoneBook.Web.Models.Reports
{
    public class ReportLocationViewModel
    {
        public int Id { get; set; }

        public int ReportId { get; set; }

        public string LocationName { get; set; }

        public int PersonCount { get; set; }

        public long PhoneNumberCount { get; set; }
    }
}
