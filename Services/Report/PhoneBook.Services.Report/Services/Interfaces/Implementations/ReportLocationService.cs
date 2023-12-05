using AutoMapper;
using PhoneBook.Services.Report.Dtos;
using PhoneBook.Services.Report.Repositories.Interfaces;
using PhoneBook.Services.Report.Services.Interfaces;
using PhoneBook.Shared.Dtos;

namespace PhoneBook.Services.Report.Services.Interfaces.Implementations
{
    public class ReportLocationService : IReportLocationService
    {
        private readonly IReportLocationRepository _reportLocationRepository;
        private readonly IMapper _mapper;


        public ReportLocationService(IMapper mapper, IReportLocationRepository reportLocationRepository)
        {
            _reportLocationRepository = reportLocationRepository;
            _mapper = mapper;
        }
        public async Task<Response<List<Models.ReportLocation>>> GetAllAsyncReportId(int reportId)
        {
            var reports = await _reportLocationRepository.GetAllByReportId(reportId);
            return Response<List<Models.ReportLocation>>.Success(_mapper.Map<List<Models.ReportLocation>>(reports.ToList()), 200);
        }
        public async Task<Response<Models.ReportLocation>> CreateAsync(Models.ReportLocation reportLocation)
        {
            var reportId = await _reportLocationRepository.Create(reportLocation);
            return Response<Models.ReportLocation>.Success(new Models.ReportLocation() { Id = reportId }, 204);
        }
    }
}
