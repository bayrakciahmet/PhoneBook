﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using PhoneBook.Services.Report.Dtos;
using PhoneBook.Services.Report.Repositories.Report;
using PhoneBook.Services.Report.Services.Report;

namespace PhoneBook.Services.Report.Test.Services
{
    public class ReportServiceTests
    {
        private readonly Mock<IReportRepository> _mockReportRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReportService _reportService;
        public ReportServiceTests()
        {
            _mockReportRepository = new Mock<IReportRepository>();
            _mockMapper = new Mock<IMapper>();
            _reportService = new ReportService(_mockMapper.Object, _mockReportRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnReports_WhenRepositoryHasData()
        {
            // Arrange
            var fakeReports = new List<Models.Report>
        {
            new Models.Report { Id = 1, ReportName = "Report 1", Status = "Tamamlandı" },
            new Models.Report { Id = 2, ReportName = "Report 2", Status = "Hazırlanıyor" }
        };

            var fakeMappedReports = new List<ReportDto>
        {
            new ReportDto { Id = 1, ReportName = "Report 1", Status = "Tamamlandı" },
            new ReportDto { Id = 2, ReportName = "Report 2", Status = "Hazırlanıyor" }
        };
            _mockReportRepository.Setup(repo => repo.GetAll()).ReturnsAsync(fakeReports);
            _mockMapper.Setup(mapper => mapper.Map<List<ReportDto>>(fakeReports)).Returns(fakeMappedReports);

            // Act
            var response = await _reportService.GetAllAsync();

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(fakeMappedReports, response.Data);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnReport_WhenRepositoryHasData()
        {
            // Arrange
            var fakeReportId = 1;
            var fakeReport = new Models.Report { Id = fakeReportId, ReportName = "Report 1", Status = "Tamamlandı" };
            var fakeMappedReport = new ReportDto { Id = fakeReportId, ReportName = "Report 1", Status = "Tamamlandı" };

            _mockReportRepository.Setup(repo => repo.GetById(fakeReportId)).ReturnsAsync(fakeReport);
            _mockMapper.Setup(mapper => mapper.Map<ReportDto>(fakeReport)).Returns(fakeMappedReport);

            // Act
            var response = await _reportService.GetByIdAsync(fakeReportId);

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(fakeMappedReport, response.Data);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnSuccessResponse_WhenReportIsCreated()
        {
            // Arrange
            var fakeReportCreateDto = new ReportCreateDto { ReportName = "New Report", Status = "Hazırlanıyor" };
            var fakeReportId = 1;

            _mockMapper.Setup(mapper => mapper.Map<Models.Report>(fakeReportCreateDto)).Returns(new Models.Report());
            _mockReportRepository.Setup(repo => repo.Create(It.IsAny<Models.Report>())).ReturnsAsync(fakeReportId);

            // Act
            var response = await _reportService.CreateAsync(fakeReportCreateDto);

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
            Assert.Equal(fakeReportId, response.Data.Id);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNoContent_WhenRepositoryDeletesReport()
        {
            // Arrange
            var fakeReportId = 1;
            _mockReportRepository.Setup(repo => repo.Delete(fakeReportId)).ReturnsAsync(1);

            // Act
            var response = await _reportService.DeleteAsync(fakeReportId);

            // Assert
            Assert.True(response.IsSuccessful);
            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
        }
    }
}