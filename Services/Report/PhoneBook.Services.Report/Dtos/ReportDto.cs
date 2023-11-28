namespace PhoneBook.Services.Report.Dtos
{
    public class ReportDto
    {
        public int Id { get; set; }

        public string ReportName { get; set; }
        public DateTime RequestDate { get; set; }

        public string Status { get; set; }
    }
}
