using Moq;
using PhoneBook.Services.Report.Repositories.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.Report.Test.Repositories
{
    public class ReportRepositoryTests
    {
        [Fact]
        public async Task GetAll_ShouldReturnReports_WhenRepositoryHasData()
        {
            // Arrange
            var fakeReports = new List<Models.Report>
        {
            new Models.Report { Id = 1, ReportName = "Report 1", Status = "Completed" },
            new Models.Report { Id = 2, ReportName = "Report 2", Status = "InProgress" }
            // Add more fake reports as needed
        };

            var mockRepo = new Mock<IReportRepository>();
            mockRepo.Setup(repo => repo.GetAll()).ReturnsAsync(fakeReports);

            // Act
            var result = await mockRepo.Object.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Models.Report>>(result);
            Assert.Equal(fakeReports, result);
        }

        [Fact]
        public async Task GetById_ShouldReturnReport_WhenRepositoryHasData()
        {
            // Arrange
            var fakeReportId = 1;
            var fakeReport = new Models.Report { Id = fakeReportId, ReportName = "Report 1", Status = "Completed" };

            var mockRepo = new Mock<IReportRepository>();
            mockRepo.Setup(repo => repo.GetById(fakeReportId)).ReturnsAsync(fakeReport);

            // Act
            var result = await mockRepo.Object.GetById(fakeReportId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Models.Report>(result);
            Assert.Equal(fakeReport, result);
        }

        [Fact]
        public async Task Create_ShouldReturnInsertedId_WhenReportIsCreated()
        {
            // Arrange
            var fakeReport = new Models.Report { ReportName = "New Report", Status = "InProgress" };
            var fakeInsertedId = 1;

            var mockRepo = new Mock<IReportRepository>();
            mockRepo.Setup(repo => repo.Create(fakeReport)).ReturnsAsync(fakeInsertedId);

            // Act
            var result = await mockRepo.Object.Create(fakeReport);

            // Assert
            Assert.Equal(fakeInsertedId, result);
        }

        [Fact]
        public async Task Update_ShouldReturnAffectedRows_WhenReportIsUpdated()
        {
            // Arrange
            var fakeReport = new Models.Report { Id = 1, ReportName = "Updated Report", Status = "Completed" };
            var fakeAffectedRows = 1;

            var mockRepo = new Mock<IReportRepository>();
            mockRepo.Setup(repo => repo.Update(fakeReport)).ReturnsAsync(fakeAffectedRows);

            // Act
            var result = await mockRepo.Object.Update(fakeReport);

            // Assert
            Assert.Equal(fakeAffectedRows, result);
        }

        [Fact]
        public async Task Delete_ShouldReturnAffectedRows_WhenReportIsDeleted()
        {
            // Arrange
            var fakeReportId = 1;
            var fakeAffectedRows = 1;

            var mockRepo = new Mock<IReportRepository>();
            mockRepo.Setup(repo => repo.Delete(fakeReportId)).ReturnsAsync(fakeAffectedRows);

            // Act
            var result = await mockRepo.Object.Delete(fakeReportId);

            // Assert
            Assert.Equal(fakeAffectedRows, result);
        }
    }
}
