namespace PhoneBook.Services.Report.Models
{
    [Dapper.Contrib.Extensions.Table("report")]
    public class Report
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public int? PersonCount { get; set; }

        public int? PhoneNumberCount { get; set; }

        public DateTime RequestDate { get; set; }

        public string Status { get; set; }
    }
}
