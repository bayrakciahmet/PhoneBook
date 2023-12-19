namespace PhoneBook.Web.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<String> GetToken();
    }
}
