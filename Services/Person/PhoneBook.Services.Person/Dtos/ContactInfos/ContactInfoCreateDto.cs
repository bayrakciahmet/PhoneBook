namespace PhoneBook.Services.Person.Dtos.ContactInfos
{
    public class ContactInfoCreateDto
    {
        public string PersonId { get; set; }

        public string InfoType { get; set; }

        public string InfoContent { get; set; }
    }
}
