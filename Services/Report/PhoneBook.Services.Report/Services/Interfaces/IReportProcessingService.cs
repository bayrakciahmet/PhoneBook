using PhoneBook.Shared.Messages;

namespace PhoneBook.Services.Report.Services.Interfaces
{
    public interface IReportProcessingService
    {
        Task ProcessCreateReportMessageCommand(CreateReportMessageCommand command);
    }
}
