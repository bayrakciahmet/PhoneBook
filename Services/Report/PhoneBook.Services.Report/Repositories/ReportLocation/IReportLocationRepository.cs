namespace PhoneBook.Services.Report.Repositories.ReportLocation
{
    public interface IReportLocationRepository
    {
        Task<IEnumerable<Models.ReportLocation>> GetAllByReportId(int reportId);
        Task<int> Create(Models.ReportLocation report);
    }
}
