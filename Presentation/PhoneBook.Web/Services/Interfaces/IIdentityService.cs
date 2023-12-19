using IdentityModel.Client;
using PhoneBook.Shared.Dtos;
using PhoneBook.Web.Models;

namespace PhoneBook.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput signinInput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
