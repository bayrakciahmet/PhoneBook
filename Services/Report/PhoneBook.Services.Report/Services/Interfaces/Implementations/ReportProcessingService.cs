using PhoneBook.Services.Report.Repositories.Interfaces;
using PhoneBook.Shared.Messages;
using System.Net.Http.Headers;

namespace PhoneBook.Services.Report.Services.Interfaces.Implementations
{
    public class ReportProcessingService : IReportProcessingService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportLocationRepository _reportLocationRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ReportProcessingService(
            IReportLocationRepository reportLocationRepository,
            IReportRepository reportRepository,
            IConfiguration configuration,
            HttpClient httpClient)
        {
            _reportLocationRepository = reportLocationRepository;
            _reportRepository = reportRepository;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task ProcessCreateReportMessageCommand(CreateReportMessageCommand command)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", command.AccessToken);
            var response = await _httpClient.GetAsync($"{_configuration["ApiGatewayUrl"]}/person/api/ContactInfos/GetReport");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<Shared.Dtos.Response<List<Models.ReportLocation>>>();
                if (content != null && content.Data != null)
                {
                    foreach (var item in content.Data)
                    {
                        item.ReportId = command.ReportId;
                        await _reportLocationRepository.Create(item);
                    }

                    var report = await _reportRepository.GetById(command.ReportId);
                    if (report != null)
                    {
                        await _reportRepository.Update(new Models.Report() { Id = command.ReportId, Status = "Tamamlandı", ReportName = report.ReportName });
                    }
                }
            }
        }
    }
}
