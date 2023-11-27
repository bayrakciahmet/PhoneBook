namespace PhoneBook.Services.Report.Repositories
{
    public interface IReportRepository
    {
        Task<IEnumerable<Models.Report>> GetAll();

        Task<Models.Report?> GetById(int id);

        Task<int> Create(Models.Report discount);

        Task<int> Update(Models.Report discount);

        Task<int> Delete(int id);
    }
}
