using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using PhoneBook.Services.Report.Repositories.ReportLocation;
using PhoneBook.Services.Report.Services.ReportLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Services.Report.Test.Services
{
    public class ReportLocationServiceTests
    {
        private readonly Mock<IReportLocationRepository> _mockReportLocationRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReportLocationService _reportLocationService;

        public ReportLocationServiceTests()
        {
            _mockReportLocationRepository = new Mock<IReportLocationRepository>();
            _mockMapper = new Mock<IMapper>();
            _reportLocationService = new ReportLocationService(_mockMapper.Object, _mockReportLocationRepository.Object);
        }

        [Fact]
        public async Task GetAllAsyncReportId_ShouldReturnMappedReportLocations_WhenRepositoryHasData()
        {
            // Arrange
            var fakeReportId = 1;
            var fakeReportLocations = new List<Models.ReportLocation>
        {
            new Models.ReportLocation { Id = 1, ReportId = fakeReportId, LocationName = "Konya", PersonCount = 10, PhoneNumberCount = 5 },
            new Models.ReportLocation { Id = 2, ReportId = fakeReportId, LocationName = "Ankara", PersonCount = 15, PhoneNumberCount = 8 }
            // Add more fake report locations as needed
        };

            var fakeMappedReportLocations = new List<Models.ReportLocation>
        {
            new Models.ReportLocation { Id = 1, ReportId = fakeReportId, LocationName = "Konya", PersonCount = 10, PhoneNumberCount = 5 },
            new Models.ReportLocation { Id = 2, ReportId = fakeReportId, LocationName = "Ankara", PersonCount = 15, PhoneNumberCount = 8 }
            // Create corresponding fake DTOs
        };

            _mockReportLocationRepository.Setup(repo => repo.GetAllByReportId(fakeReportId)).ReturnsAsync(fakeReportLocations);
            _mockMapper.Setup(mapper => mapper.Map<List<Models.ReportLocation>>(fakeReportLocations)).Returns(fakeMappedReportLocations);

            // Act
            var response = await _reportLocationService.GetAllAsyncReportId(fakeReportId);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccessful);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(fakeMappedReportLocations, response.Data);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnSuccessResponse_WhenReportLocationIsCreated()
        {
            // Arrange
            var fakeReportLocation = new Models.ReportLocation { ReportId = 1, LocationName = "İstanbul", PersonCount = 10, PhoneNumberCount = 5 };
            var fakeReportLocationId = 1;

            _mockMapper.Setup(mapper => mapper.Map<Models.ReportLocation>(fakeReportLocation)).Returns(new Models.ReportLocation());
            _mockReportLocationRepository.Setup(repo => repo.Create(It.IsAny<Models.ReportLocation>())).ReturnsAsync(fakeReportLocationId);

            // Act
            var response = await _reportLocationService.CreateAsync(fakeReportLocation);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.IsSuccessful);
            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.Equal(fakeReportLocationId, response.Data.Id);
        }
    }
}
