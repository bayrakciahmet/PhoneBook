namespace PhoneBook.Services.Person.Dtos.Persons
{
    public class PersonDto
    {
        public string UUID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Company { get; set; }

        public long? ContactInfoCount { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
