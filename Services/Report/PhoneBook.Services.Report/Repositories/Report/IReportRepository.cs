namespace PhoneBook.Services.Report.Repositories.Report
{
    public interface IReportRepository
    {
        Task<IEnumerable<Models.Report>> GetAll();

        Task<Models.Report?> GetById(int id);

        Task<int> Create(Models.Report report);

        Task<int> Update(Models.Report report);

        Task<int> Delete(int id);
    }
}
