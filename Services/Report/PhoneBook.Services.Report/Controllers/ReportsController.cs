using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Services.Report.Dtos;
using PhoneBook.Services.Report.Services.Interfaces;
using PhoneBook.Shared.ControllerBases;
using PhoneBook.Shared.Messages;

namespace PhoneBook.Services.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : CustomBaseController
    {
        private readonly IReportService _reportService;
        private readonly IReportLocationService _reportLocationService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public ReportsController(IReportService reportService, ISendEndpointProvider sendEndpointProvider, IReportLocationService reportLocationService)
        {
            _reportService = reportService;
            _sendEndpointProvider = sendEndpointProvider;
            _reportLocationService = reportLocationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await _reportService.GetAllAsync();
            return CreateActionResultInstance(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await _reportService.GetByIdAsync(id);
            return CreateActionResultInstance(report);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReportCreateDto reportCreateDto)
        {
            reportCreateDto.Status = "Hazırlanıyor";
            var report = await _reportService.CreateAsync(reportCreateDto);
            if (report.IsSuccessful==true)
            {
                var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-report-service"));
                CreateReportMessageCommand createReportMessageCommand = new CreateReportMessageCommand()
                {
                    ReportId = report.Data.Id
                };
                await sendEndpoint.Send<CreateReportMessageCommand>(createReportMessageCommand);
            }            
            return CreateActionResultInstance(report);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ReportUpdateDto reportUpdateDto)
        {
            return CreateActionResultInstance(await _reportService.UpdateAsync(reportUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _reportService.DeleteAsync(id));
        }

        [HttpGet]
        [Route("/api/[controller]/GetAllReportById/{reportId}")]
        public async Task<IActionResult> GetAllReportById(int reportId)
        {
            var report = await _reportLocationService.GetAllAsyncReportId(reportId);
            return CreateActionResultInstance(report);
        }
    }
}
