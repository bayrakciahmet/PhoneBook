using PhoneBook.Web.Models.ContactInfos;

namespace PhoneBook.Web.Services.ContactInfo
{
    public interface IContactInfoService
    {
        Task<List<ContactInfoViewModel>> GetAllContactInfoAsync();

        Task<List<ContactInfoViewModel>> GetAllContactInfoPersonIdAsync(string personId);

        Task<ContactInfoViewModel> GetByContactInfoId(string id);

        Task<bool> CreateContactInfoAsync(ContactInfoCreateInput contactInfoCreateInput);

        Task<bool> UpdateContactInfoAsync(ContactInfoUpdateInput contactInfoUpdateInput);

        Task<bool> DeleteContactInfoAsync(string id);
    }
}
