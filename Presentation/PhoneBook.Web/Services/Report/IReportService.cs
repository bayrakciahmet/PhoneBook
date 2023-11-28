using PhoneBook.Web.Models.Reports;

namespace PhoneBook.Web.Services.Report
{
    public interface IReportService
    {
        Task<List<ReportViewModel>> GetAllReportAsync();

        Task<ReportViewModel> GetByReportId(string reportId);

        Task<bool> CreateReportAsync(ReportCreateInput reportCreateInput);

        Task<bool> UpdateReportAsync(ReportUpdateInput reportUpdateInput);

        Task<bool> DeleteReportAsync(string reportId);
    }
}
