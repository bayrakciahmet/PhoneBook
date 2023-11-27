using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.Services.Report.Dtos;
using PhoneBook.Shared.Dtos;

namespace PhoneBook.Services.Report.Services
{
    public interface IReportService
    {
        Task<Response<List<ReportDto>>> GetAllAsync();

        Task<Response<ReportDto>> GetByIdAsync(int id);

        Task<Response<NoContent>> CreateAsync(ReportCreateDto reportCreateDto);

        Task<Response<NoContent>> UpdateAsync(ReportUpdateDto reportUpdateDto);

        Task<Response<NoContent>> DeleteAsync(int id);

    }
}
