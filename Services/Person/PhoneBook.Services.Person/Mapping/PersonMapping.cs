using AutoMapper;
using PhoneBook.Services.Person.Dtos.Persons;
namespace PhoneBook.Services.Person.Mapping
{
    public class PersonMapping : Profile
    {
        public PersonMapping()
        {
            CreateMap<Models.Person, PersonDto>().ReverseMap();
            CreateMap<Models.Person, PersonCreateDto>().ReverseMap();
            CreateMap<Models.Person, PersonUpdateDto>().ReverseMap();
        }
    }
}
