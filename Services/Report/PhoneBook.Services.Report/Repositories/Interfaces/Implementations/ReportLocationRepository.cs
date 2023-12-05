using Dapper;
using PhoneBook.Services.Report.Repositories.Interfaces;
using System.Data;

namespace PhoneBook.Services.Report.Repositories.Interfaces.Implementations
{
    public class ReportLocationRepository : IReportLocationRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly IConfiguration _configuration;
        public ReportLocationRepository(IDbConnection dbConnection, IConfiguration configuration)
        {

            _configuration = configuration;
            _dbConnection = dbConnection;//new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<IEnumerable<Models.ReportLocation>> GetAllByReportId(int ReportId)
        {
            return await _dbConnection.QueryAsync<Models.ReportLocation>("SELECT * FROM reportlocation where reportid=@ReportId", new { ReportId });
        }
        public async Task<int> Create(Models.ReportLocation report)
        {
            string insertQuery = "INSERT INTO reportlocation (reportid, locationname,personcount,phonenumbercount) VALUES (@ReportId, @LocationName,@PersonCount,@PhoneNumberCount) RETURNING Id";
            var insertedId = await _dbConnection.ExecuteScalarAsync<int>(insertQuery, new { report.ReportId, report.LocationName, report.PersonCount, report.PhoneNumberCount });

            return insertedId;
        }
    }
}
