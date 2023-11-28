using PhoneBook.Web.Models.Persons;

namespace PhoneBook.Web.Services.Person
{
    public interface IPersonService
    {
        Task<List<PersonViewModel>> GetAllPersonAsync();

        Task<PersonViewModel> GetByPersonId(string personId);

        Task<bool> CreatePersonAsync(PersonCreateInput personCreateInput);

        Task<bool> UpdatePersonAsync(PersonUpdateInput personUpdateInput);

        Task<bool> DeletePersonAsync(string personId);
    }
}
