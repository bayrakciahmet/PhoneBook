using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.Shared.Dtos;
using PhoneBook.Web.Models.ContactInfos;

namespace PhoneBook.Web.Services.Interfaces
{
    public interface IContactInfoService
    {
        Task<List<ContactInfoViewModel>> GetAllContactInfoAsync();

        Task<List<ContactInfoViewModel>> GetAllContactInfoPersonIdAsync(string personId);

        Task<ContactInfoViewModel> GetByContactInfoId(string id);

        Task<Response<ContactInfoViewModel>> CreateContactInfoAsync(ContactInfoCreateInput contactInfoCreateInput);

        Task<Response<NoContent>> UpdateContactInfoAsync(ContactInfoUpdateInput contactInfoUpdateInput);

        Task<bool> DeleteContactInfoAsync(string id);
    }
}
