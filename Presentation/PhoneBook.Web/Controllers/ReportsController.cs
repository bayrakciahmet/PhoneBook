using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Web.Models.Reports;
using PhoneBook.Web.Services.Interfaces;

namespace PhoneBook.Web.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _reportService.GetAllReportAsync();
            return View(model);
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

        public async Task<IActionResult> ReportLocationList(int reportId)
        {
            return PartialView("_ContactInfoList", await _reportService.GetAllReportLocationById(reportId));
        }

        public async Task<IActionResult> Update(int id)
        {
            var report = await _reportService.GetByReportId(id);
            if (report == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var reportLocations = await _reportService.GetAllReportLocationById(report.Id);
            ReportUpdateInput reportUpdateInput = new ReportUpdateInput()
            {
                Id = report.Id,
                ReportName = report.ReportName,
                Status = report.Status,
                reportLocations = reportLocations
            };

            return View(reportUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ReportUpdateInput reportUpdateInput)
        {
            if (!ModelState.IsValid)
            {
                return View(reportUpdateInput);
            }
            var report = await _reportService.GetByReportId(reportUpdateInput.Id);
            if (report == null)
                return RedirectToAction("Index", "Home");
            reportUpdateInput.Status = report.Status;
            await _reportService.UpdateReportAsync(reportUpdateInput);
            return RedirectToAction("Index", "Reports");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _reportService.DeleteReportAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
