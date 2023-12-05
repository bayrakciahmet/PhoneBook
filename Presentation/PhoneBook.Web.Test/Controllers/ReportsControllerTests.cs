using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Web.Controllers;
using PhoneBook.Web.Models.Reports;
using PhoneBook.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Web.Test.Controllers
{
    public class ReportsControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewWithModel()
        {
            // Arrange
            var reportServiceMock = new Mock<IReportService>();
            var reportsController = new ReportsController(reportServiceMock.Object);

            var fakeReports = new List<ReportViewModel> { };
            reportServiceMock.Setup(service => service.GetAllReportAsync()).ReturnsAsync(fakeReports);

            // Act
            var result = await reportsController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<ReportViewModel>>(viewResult.Model);
            Assert.Equal(fakeReports, model);
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsView()
        {
            // Arrange
            var reportServiceMock = new Mock<IReportService>();
            var reportsController = new ReportsController(reportServiceMock.Object);

            reportsController.ModelState.AddModelError("key", "error message");

            // Act
            var result = await reportsController.Create(new ReportCreateInput());

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var reportServiceMock = new Mock<IReportService>();
            var reportsController = new ReportsController(reportServiceMock.Object);

            var fakeReportCreateInput = new ReportCreateInput {  };
            reportServiceMock.Setup(service => service.CreateReportAsync(It.IsAny<ReportCreateInput>())).ReturnsAsync(true);

            // Act
            var result = await reportsController.Create(fakeReportCreateInput);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Update_InvalidModel_ReturnsView()
        {
            // Arrange
            var reportServiceMock = new Mock<IReportService>();
            var reportsController = new ReportsController(reportServiceMock.Object);

            reportsController.ModelState.AddModelError("key", "error message");

            // Act
            var result = await reportsController.Update(new ReportUpdateInput());

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Update_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var reportServiceMock = new Mock<IReportService>();
            var reportsController = new ReportsController(reportServiceMock.Object);

            var fakeReportUpdateInput = new ReportUpdateInput {  };
            reportServiceMock.Setup(service => service.GetByReportId(fakeReportUpdateInput.Id)).ReturnsAsync(new ReportViewModel());
            reportServiceMock.Setup(service => service.UpdateReportAsync(It.IsAny<ReportUpdateInput>())).ReturnsAsync(true);

            // Act
            var result = await reportsController.Update(fakeReportUpdateInput);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Delete_CallsDeleteReportAsyncAndRedirectsToIndex()
        {
            // Arrange
            var reportServiceMock = new Mock<IReportService>();
            var reportsController = new ReportsController(reportServiceMock.Object);

            var fakeReportId = "fakeId";
            reportServiceMock.Setup(service => service.DeleteReportAsync(fakeReportId)).ReturnsAsync(true);

            // Act
            var result = await reportsController.Delete(fakeReportId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            reportServiceMock.Verify(service => service.DeleteReportAsync(fakeReportId), Times.Once);
        }

        [Fact]
        public async Task GetByReportId_ReturnsNotFound_WhenReportNotFound()
        {
            // Arrange
            var reportServiceMock = new Mock<IReportService>();
            var reportsController = new ReportsController(reportServiceMock.Object);

            int fakeReportId = 1;
            reportServiceMock.Setup(service => service.GetByReportId(fakeReportId)).ReturnsAsync((ReportViewModel)null);

            // Act
            var result = await reportsController.Update(fakeReportId);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async Task Update_InvalidModel_ReturnsViewWithReportLocations()
        {
            // Arrange
            var reportServiceMock = new Mock<IReportService>();
            var reportsController = new ReportsController(reportServiceMock.Object);

            var fakeReportUpdateInput = new ReportUpdateInput {  };
            reportsController.ModelState.AddModelError("key", "error message");

            // Act
            var result = await reportsController.Update(fakeReportUpdateInput);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ReportUpdateInput>(viewResult.Model);
            Assert.Equal(fakeReportUpdateInput, model);
        }

        [Fact]
        public async Task ReportLocationList_ReturnsPartialViewWithModel()
        {
            // Arrange
            var reportServiceMock = new Mock<IReportService>();
            var reportsController = new ReportsController(reportServiceMock.Object);

            int fakeReportId = 1;
            var fakeReportLocations = new List<ReportLocationViewModel> {  };
            reportServiceMock.Setup(service => service.GetAllReportLocationById(fakeReportId)).ReturnsAsync(fakeReportLocations);

            // Act
            var result = await reportsController.ReportLocationList(fakeReportId);

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Equal("_ContactInfoList", partialViewResult.ViewName);
            var model = Assert.IsAssignableFrom<List<ReportLocationViewModel>>(partialViewResult.Model);
            Assert.Equal(fakeReportLocations, model);
        }
    }
}
