namespace PhoneBook.Services.Person.Dtos.ContactInfos
{
    public class ContactInfoDto
    {
        public string UUID { get; set; }

        public string? PersonId { get; set; }

        public string? InfoType { get; set; }

        public string? InfoContent { get; set; }

        public DateTime ModifiedTime { get; set; }
    }
}
