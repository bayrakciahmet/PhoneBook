using PhoneBook.Services.Report.Models;
using PhoneBook.Shared.Dtos;

namespace PhoneBook.Services.Report.Services.Interfaces
{
    public interface IReportLocationService
    {
        Task<Response<List<Models.ReportLocation>>> GetAllAsyncReportId(int id);
        Task<Response<Models.ReportLocation>> CreateAsync(Models.ReportLocation reportLocation);
    }
}
