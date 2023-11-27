using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Services.Report.Dtos;
using PhoneBook.Services.Report.Services;
using PhoneBook.Shared.ControllerBases;

namespace PhoneBook.Services.Report.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : CustomBaseController
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
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
            var discount = await _reportService.GetByIdAsync(id);
            return CreateActionResultInstance(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReportCreateDto reportCreateDto)
        {
            return CreateActionResultInstance(await _reportService.CreateAsync(reportCreateDto));
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
    }
}
