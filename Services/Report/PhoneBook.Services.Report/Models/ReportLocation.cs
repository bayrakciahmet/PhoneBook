﻿namespace PhoneBook.Services.Report.Models
{
    public class ReportLocation
    {
        public int Id { get; set; } 

        public int ReportId { get; set; }

        public string LocationName { get; set; }

        public int PersonCount { get; set; }

        public long PhoneNumberCount { get; set; }
    }
}
