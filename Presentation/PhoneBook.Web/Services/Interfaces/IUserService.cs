using PhoneBook.Web.Models;

namespace PhoneBook.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
