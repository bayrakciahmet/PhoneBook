using Dapper;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Services.Report.Repositories.Interfaces;
using System.Data;

namespace PhoneBook.Services.Report.Repositories.Interfaces.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IConfiguration _configuration;
        public ReportRepository(IDbConnection dbConnection, IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = dbConnection;//new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<IEnumerable<Models.Report>> GetAll()
        {
            return await _dbConnection.QueryAsync<Models.Report>("SELECT * FROM report ORDER BY requestdate DESC;");
        }

        public async Task<Models.Report?> GetById(int id)
        {
            return (await _dbConnection.QueryAsync<Models.Report>("select * from report where id=@Id", new { Id = id })).SingleOrDefault();
        }

        public async Task<int> Create(Models.Report report)
        {
            string insertQuery = "INSERT INTO report (reportname, status) VALUES (@ReportName, @Status) RETURNING Id";
            var insertedId = await _dbConnection.ExecuteScalarAsync<int>(insertQuery, new { report.ReportName, Status = "Hazırlanıyor" });

            return insertedId;
            //return await _dbConnection.ExecuteAsync("INSERT INTO report (reportname,status) VALUES(@ReportName,@Status)", new { ReportName= report.ReportName, Status = "Hazırlanıyor" });
        }

        public async Task<int> Update(Models.Report report)
        {
            return await _dbConnection.ExecuteAsync("UPDATE report SET status=@Status , reportname=@ReportName where id=@Id", new { report.Status, report.ReportName, report.Id });
        }

        public async Task<int> Delete(int id)
        {
            return await _dbConnection.ExecuteAsync("delete from report where id=@Id", new { Id = id });
        }
    }
}
