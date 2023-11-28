using Microsoft.AspNetCore.Mvc;
using PhoneBook.Web.Models.Reports;
using PhoneBook.Web.Services.Report;

namespace PhoneBook.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _reportService.GetAllReportAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ReportCreateInput reportCreateInput)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var respons = await _reportService.CreateReportAsync(reportCreateInput);
            return RedirectToAction(nameof(Index));
        }
    }
}
