using System.Text.Json.Serialization;

namespace PhoneBook.Web.Models.Persons
{
    public class PersonViewModel
    {
        public string UUID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Company { get; set; }

        public DateTime CreatedTime { get; set; }

        public long? ContactInfoCount { get; set; }
    }
}
