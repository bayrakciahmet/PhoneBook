using AutoMapper;
using PhoneBook.Services.Report.Dtos;

namespace PhoneBook.Services.Report.Mapping
{
    public class ReportMapping :Profile
    {
        public ReportMapping()
        {
            CreateMap<Models.Report, ReportDto>().ReverseMap();
            CreateMap<Models.Report, ReportCreateDto>().ReverseMap();
            CreateMap<Models.Report, ReportUpdateDto>().ReverseMap();
        }
    }
}
