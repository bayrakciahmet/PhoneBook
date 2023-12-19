namespace PhoneBook.Web.Models
{
    public class ServiceApiSettings
    {
        public string IdentityBaseUri { get; set; }
        public string GatewayBaseUri { get; set; }

        public ServiceApi Person { get; set; }

        public ServiceApi Report { get; set; }
    }

    public class ServiceApi
    {
        public string Path { get; set; }
    }
}
