using Moq;
using PhoneBook.Services.Report.Repositories.ReportLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.Report.Test.Repositories
{
    public class ReportLocationRepositoryTests
    {
        [Fact]
        public async Task GetAllByReportId_ShouldReturnReportLocations_WhenRepositoryHasData()
        {
            // Arrange
            var fakeReportId = 1;
            var fakeReportLocations = new List<Models.ReportLocation>
        {
            new Models.ReportLocation { Id = 1, ReportId = fakeReportId, LocationName = "Ankara", PersonCount = 10, PhoneNumberCount = 5 },
            new Models.ReportLocation { Id = 2, ReportId = fakeReportId, LocationName = "İstanbul", PersonCount = 15, PhoneNumberCount = 8 }
        };

            var mockRepo = new Mock<IReportLocationRepository>();
            mockRepo.Setup(repo => repo.GetAllByReportId(fakeReportId)).ReturnsAsync(fakeReportLocations);

            // Act
            var result = await mockRepo.Object.GetAllByReportId(fakeReportId);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Models.ReportLocation>>(result);
            Assert.Equal(fakeReportLocations, result);
        }

        [Fact]
        public async Task Create_ShouldReturnInsertedId_WhenReportLocationIsCreated()
        {
            // Arrange
            var fakeReportLocation = new Models.ReportLocation { ReportId = 1, LocationName = "New Location", PersonCount = 10, PhoneNumberCount = 5 };
            var fakeInsertedId = 1;

            var mockRepo = new Mock<IReportLocationRepository>();
            mockRepo.Setup(repo => repo.Create(fakeReportLocation)).ReturnsAsync(fakeInsertedId);

            // Act
            var result = await mockRepo.Object.Create(fakeReportLocation);

            // Assert
            Assert.Equal(fakeInsertedId, result);
        }
    }
}
