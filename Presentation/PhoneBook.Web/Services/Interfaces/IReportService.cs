using PhoneBook.Web.Models.Reports;

namespace PhoneBook.Web.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<ReportViewModel>> GetAllReportAsync();

        Task<List<ReportLocationViewModel>> GetAllReportLocationById(int reportId);
        Task<ReportViewModel> GetByReportId(int reportId);

        Task<bool> CreateReportAsync(ReportCreateInput reportCreateInput);

        Task<bool> UpdateReportAsync(ReportUpdateInput reportUpdateInput);

        Task<bool> DeleteReportAsync(string reportId);
    }
}
