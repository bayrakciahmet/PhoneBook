using MassTransit;
using PhoneBook.Services.Report.Repositories.Interfaces;
using PhoneBook.Services.Report.Services.Interfaces;
using PhoneBook.Shared.Messages;
using System.Net.Http.Headers;

namespace PhoneBook.Services.Report.Consumers
{
    public class CreateReportMessageCommandConsumer : IConsumer<CreateReportMessageCommand>
    {
        private readonly IReportProcessingService _reportProcessingService;

        public CreateReportMessageCommandConsumer(IReportProcessingService reportProcessingService)
        {
            _reportProcessingService = reportProcessingService;
        }

        public async Task Consume(ConsumeContext<CreateReportMessageCommand> context)
        {
            await _reportProcessingService.ProcessCreateReportMessageCommand(context.Message);
        }
    }
}
