namespace PhoneBook.Services.Report.Repositories.Interfaces
{
    public interface IReportLocationRepository
    {
        Task<IEnumerable<Models.ReportLocation>> GetAllByReportId(int reportId);
        Task<int> Create(Models.ReportLocation report);
    }
}
