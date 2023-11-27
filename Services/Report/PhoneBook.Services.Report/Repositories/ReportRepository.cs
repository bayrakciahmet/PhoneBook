using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Npgsql;
using PhoneBook.Shared.Dtos;
using System.Data;

namespace PhoneBook.Services.Report.Repositories
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
            return await _dbConnection.QueryAsync<Models.Report>("SELECT * FROM report");
        }

        public async Task<Models.Report?> GetById(int id)
        {
            return (await _dbConnection.QueryAsync<Models.Report>("select * from report where id=@Id", new { Id = id })).SingleOrDefault();
        }

        public async Task<int> Create(Models.Report report)
        {
            return await _dbConnection.ExecuteAsync("INSERT INTO report (location,status) VALUES(@Location,@Status)", new { Location = report.Location, Status = "Hazırlanıyor" });
        }

        public async Task<int> Update(Models.Report report)
        {
            return await _dbConnection.ExecuteAsync("UPDATE report SET location=@Location, status=@Status ,personCount=@PersonCount, phonenumbercount=@PhoneNumberCount where id=@Id", new { Location = report.Location, Status = report.Status,Id=report.Id , PersonCount =report.PersonCount, PhoneNumberCount =report.PhoneNumberCount});
        }

        public async Task<int> Delete(int id)
        {
            return await _dbConnection.ExecuteAsync("delete from report where id=@Id", new { Id = id });
        }
    }
}
