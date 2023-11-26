namespace PhoneBook.Services.Person.Dtos.Persons
{
    public class PersonUpdateDto
    {
        public string UUID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Company { get; set; }
    }
}
