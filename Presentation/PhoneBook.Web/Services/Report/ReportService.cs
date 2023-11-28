using PhoneBook.Shared.Dtos;
using PhoneBook.Web.Models.Reports;

namespace PhoneBook.Web.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _client;
        public ReportService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<ReportViewModel>> GetAllReportAsync()
        {
            var response = await _client.GetAsync("reports");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ReportViewModel>>>();

            return responseSuccess.Data;
        }

        public async Task<ReportViewModel> GetByReportId(int reportId)
        {
            var response = await _client.GetAsync($"reports/{reportId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<ReportViewModel>>();

            return responseSuccess.Data;
        }

        public async Task<bool> CreateReportAsync(ReportCreateInput reportCreateInput)
        {
            var response = await _client.PostAsJsonAsync<ReportCreateInput>("reports", reportCreateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateReportAsync(ReportUpdateInput reportUpdateInput)
        {
            var response = await _client.PutAsJsonAsync<ReportUpdateInput>("reports", reportUpdateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteReportAsync(string reportId)
        {
            var response = await _client.DeleteAsync($"reports/{reportId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ReportLocationViewModel>> GetAllReportLocationById(int reportId)
        {
            var response = await _client.GetAsync($"reports/GetAllReportById/{reportId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ReportLocationViewModel>>>();

            return responseSuccess.Data;
        }
    }
}
