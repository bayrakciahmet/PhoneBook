namespace PhoneBook.Services.Person.Settings.Interfaces
{
    public interface IDatabaseSettings
    {
        public string PersonCollectionName { get; set; }
        public string ContactInfoCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
