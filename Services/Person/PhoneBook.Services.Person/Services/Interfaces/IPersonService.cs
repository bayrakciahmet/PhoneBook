using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.Services.Person.Dtos.Persons;
using PhoneBook.Shared.Dtos;

namespace PhoneBook.Services.Person.Services.Interfaces
{
    public interface IPersonService
    {
        Task<Response<List<PersonDto>>> GetAllAsync();
        Task<Response<PersonDto>> GetByIdAsync(string id);
        Task<Response<PersonDto>> CreateAsync(PersonCreateDto courseCreateDto);
        Task<Response<NoContent>> UpdateAsync(PersonUpdateDto courseUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
