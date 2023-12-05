using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PhoneBook.Services.Report.Controllers;
using PhoneBook.Services.Report.Dtos;
using PhoneBook.Services.Report.Services.Interfaces;
using PhoneBook.Shared.Dtos;
namespace PhoneBook.Services.Report.Test.Controllers
{
    public class ReportsControllerTests
    {
        [Fact]
        public async Task GetAll_ShouldReturnReports_WhenServiceHasData()
        {
            // Arrange
            var fakeReports = new List<ReportDto>
        {
            new ReportDto { Id = 1, ReportName = "Report 1", RequestDate = DateTime.Now, Status = "Tamamlandı" },
            new ReportDto { Id = 2, ReportName = "Report 2", RequestDate = DateTime.Now, Status = "Hazırlanıyor" }
        };

            var mockService = new Mock<IReportService>();
            mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(Response<List<ReportDto>>.Success(fakeReports, 200));

            var controller = new ReportsController(mockService.Object, null, null);

            // Act
            var result = await controller.GetAll();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<List<ReportDto>>>(objectResult.Value);

            Assert.NotNull(model);
            Assert.Equal(fakeReports, model.Data);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList_WhenServiceHasNoData()
        {
            // Arrange
            var mockService = new Mock<IReportService>();
            mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(Response<List<ReportDto>>.Success(new List<ReportDto>(), 200));

            var controller = new ReportsController(mockService.Object, null, null);

            // Act
            var result = await controller.GetAll();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<List<ReportDto>>>(objectResult.Value);

            Assert.NotNull(model);
            Assert.Empty(model.Data);
        }

        [Fact]
        public async Task GetById_ShouldReturnReport_WhenServiceHasData()
        {
            // Arrange
            var fakeReportId = 1;
            var fakeReport = new ReportDto { Id = fakeReportId, ReportName = "Report 1", RequestDate = DateTime.Now, Status = "Tamamlandı" };

            var mockService = new Mock<IReportService>();
            mockService.Setup(service => service.GetByIdAsync(fakeReportId)).ReturnsAsync(Response<ReportDto>.Success(fakeReport, 200));

            var controller = new ReportsController(mockService.Object, null, null);

            // Act
            var result = await controller.GetById(fakeReportId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<ReportDto>>(objectResult.Value);

            Assert.NotNull(model);
            Assert.Equal(fakeReport, model.Data);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenServiceReturnsNull()
        {
            // Arrange
            var fakeReportId = 1;

            var mockService = new Mock<IReportService>();
            mockService.Setup(service => service.GetByIdAsync(fakeReportId)).ReturnsAsync(Response<ReportDto>.Fail("Report not found", 404));

            var controller = new ReportsController(mockService.Object, null, null);

            // Act
            var result = await controller.GetById(fakeReportId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<ReportDto>>(objectResult.Value);

            Assert.NotNull(model);
            Assert.Equal("Report not found", model.Errors.First());
            Assert.Equal(404, model.StatusCode);
        }



        [Fact]
        public async Task Update_ShouldReturnNoContent_WhenServiceUpdatesReport()
        {
            // Arrange
            var fakeReportUpdateDto = new ReportUpdateDto { Id = 1, ReportName = "Updated Report", Status = "Tammalandı" };

            var mockService = new Mock<IReportService>();
            mockService.Setup(service => service.UpdateAsync(fakeReportUpdateDto)).ReturnsAsync(Response<NoContent>.Success(204));

            var controller = new ReportsController(mockService.Object, null, null);

            // Act
            var result = await controller.Update(fakeReportUpdateDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<NoContent>>(objectResult.Value);

            Assert.NotNull(model);
            Assert.Equal(204, model.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenServiceFailsToUpdateReport()
        {
            // Arrange
            var fakeReportUpdateDto = new ReportUpdateDto { Id = 1, ReportName = "Updated Report", Status = "Completed" };

            var mockService = new Mock<IReportService>();
            mockService.Setup(service => service.UpdateAsync(fakeReportUpdateDto)).ReturnsAsync(Response<NoContent>.Fail("Report not found", 404));

            var controller = new ReportsController(mockService.Object, null, null);

            // Act
            var result = await controller.Update(fakeReportUpdateDto);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<NoContent>>(objectResult.Value);

            Assert.NotNull(model);
            Assert.Equal("Report not found", model.Errors.First());
            Assert.Equal(404, model.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenServiceDeletesReport()
        {
            // Arrange
            var fakeReportId = 1;

            var mockService = new Mock<IReportService>();
            mockService.Setup(service => service.DeleteAsync(fakeReportId)).ReturnsAsync(Response<NoContent>.Success(204));

            var controller = new ReportsController(mockService.Object, null, null);

            // Act
            var result = await controller.Delete(fakeReportId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<NoContent>>(objectResult.Value);

            Assert.NotNull(model);
            Assert.Equal(204, model.StatusCode);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenServiceFailsToDeleteReport()
        {
            // Arrange
            var fakeReportId = 1;

            var mockService = new Mock<IReportService>();
            mockService.Setup(service => service.DeleteAsync(fakeReportId)).ReturnsAsync(Response<NoContent>.Fail("Report not found", 404));

            var controller = new ReportsController(mockService.Object, null, null);

            // Act
            var result = await controller.Delete(fakeReportId);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<Response<NoContent>>(objectResult.Value);

            Assert.NotNull(model);
            Assert.Equal("Report not found", model.Errors.First());
            Assert.Equal(404, model.StatusCode);
        }

    }
}
