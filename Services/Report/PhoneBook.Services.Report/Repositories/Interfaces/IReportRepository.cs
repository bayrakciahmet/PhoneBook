namespace PhoneBook.Services.Report.Repositories.Interfaces
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
