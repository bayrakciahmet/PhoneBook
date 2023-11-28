using MassTransit;
using PhoneBook.Services.Report.Repositories.Report;
using PhoneBook.Services.Report.Repositories.ReportLocation;
using PhoneBook.Shared.Messages;

namespace PhoneBook.Services.Report.Consumers
{
    public class CreateReportMessageCommandConsumer : IConsumer<CreateReportMessageCommand>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportLocationRepository _reportLocationRepository;
        public CreateReportMessageCommandConsumer(IReportLocationRepository reportLocationRepository, IReportRepository reportRepository)
        {
            _reportLocationRepository = reportLocationRepository;
            _reportRepository = reportRepository;
        }
        public async Task Consume(ConsumeContext<CreateReportMessageCommand> context)
        {
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:6000/services/person/ContactInfos/GetReport");
            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var content = await response.Content.ReadFromJsonAsync<Shared.Dtos.Response<List<Models.ReportLocation>>>();
                    if (content != null)
                    {
                        if (content.Data != null)
                        {
                            foreach (var item in content.Data)
                            {
                                item.ReportId = context.Message.ReportId;
                                await _reportLocationRepository.Create(item);
                            }
                            var report = _reportRepository.GetById(context.Message.ReportId);
                            await _reportRepository.Update(new Models.Report() { Id = context.Message.ReportId, Status = "Tamamlandı", ReportName = report.Result.ReportName });
                        }
                    }

                }
                catch (Exception)
                {


                }

            }

        }
    }
}
