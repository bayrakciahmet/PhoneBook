using AutoMapper;
using PhoneBook.Services.Person.Dtos.ContactInfos;
using PhoneBook.Services.Person.Models;

namespace PhoneBook.Services.Person.Mapping
{
    public class ContactInfoMapping : Profile
    {
        public ContactInfoMapping()
        {
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoCreateDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoUpdateDto>().ReverseMap();
        }
    }
}
