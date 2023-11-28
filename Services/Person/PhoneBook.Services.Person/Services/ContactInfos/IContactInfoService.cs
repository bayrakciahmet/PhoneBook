using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.Services.Person.Dtos.ContactInfos;
using PhoneBook.Services.Person.Dtos.Persons;
using PhoneBook.Shared.Dtos;

namespace PhoneBook.Services.Person.Services.ContactInfos
{
    public interface IContactInfoService
    {
        Task<Response<List<ContactInfoDto>>> GetAllAsync();
        Task<Response<ContactInfoDto>> GetByIdAsync(string id);
        Task<Response<List<ContactInfoDto>>> GetAllByPersonIdAsync(string personId);
        Task<Response<ContactInfoDto>> CreateAsync(ContactInfoCreateDto courseCreateDto);
        Task<Response<NoContent>> UpdateAsync(ContactInfoUpdateDto courseUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);

        Task<Response<List<Dtos.Report.ReportDto>>> GetReport();
    }
}
