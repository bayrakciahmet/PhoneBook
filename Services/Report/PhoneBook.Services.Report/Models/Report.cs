namespace PhoneBook.Services.Report.Models
{
    [Dapper.Contrib.Extensions.Table("report")]
    public class Report
    {
        public int Id { get; set; }

        public string ReportName { get; set; }

        public DateTime RequestDate { get; set; }

        public string Status { get; set; }
    }
}
