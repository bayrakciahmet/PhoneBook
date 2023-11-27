namespace PhoneBook.Services.Report.Dtos
{
    public class ReportUpdateDto
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public int? PersonCount { get; set; }

        public int? PhoneNumberCount { get; set; }

        public string Status { get; set; }
    }
}
