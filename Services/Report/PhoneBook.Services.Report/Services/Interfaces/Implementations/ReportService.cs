﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using PhoneBook.Services.Report.Dtos;
using PhoneBook.Services.Report.Repositories.Interfaces;
using PhoneBook.Services.Report.Validators;
using PhoneBook.Shared.Dtos;

namespace PhoneBook.Services.Report.Services.Interfaces.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        private readonly IValidator<ReportCreateDto> _reportCreateDtoValidator;
        private readonly IValidator<ReportUpdateDto> _reportUpdateDtoValidator;
        public ReportService(IMapper mapper, IReportRepository reportRepository, IValidator<ReportCreateDto> reportCreateDtoValidator, IValidator<ReportUpdateDto> reportUpdateDtoValidator)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
            _reportCreateDtoValidator = reportCreateDtoValidator;
            _reportUpdateDtoValidator = reportUpdateDtoValidator;
        }

        public async Task<Response<List<ReportDto>>> GetAllAsync()
        {
            var reports = await _reportRepository.GetAll();
            return Response<List<ReportDto>>.Success(_mapper.Map<List<ReportDto>>(reports.ToList()), 200);
        }

        public async Task<Response<ReportDto>> GetByIdAsync(int id)
        {
            var report = await _reportRepository.GetById(id);
            if (report == null)
            {
                return Response<ReportDto>.Fail("report not found", 404);
            }
            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
        }


        public async Task<Response<ReportDto>> CreateAsync(ReportCreateDto reportCreateDto)
        {
            var validationResult = await _reportCreateDtoValidator.ValidateAsync(reportCreateDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Response<ReportDto>.Fail(errors, 400);
            }
            var reportId = await _reportRepository.Create(_mapper.Map<Models.Report>(reportCreateDto));
            return Response<ReportDto>.Success(new ReportDto() { Id = reportId }, 204);
        }

        public async Task<Response<NoContent>> UpdateAsync(ReportUpdateDto reportUpdateDto)
        {
            var validationResult = await _reportUpdateDtoValidator.ValidateAsync(reportUpdateDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Response<NoContent>.Fail(errors, 400);
            }
            var saveStatus = await _reportRepository.Update(_mapper.Map<Models.Report>(reportUpdateDto));
            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("an error accured while adding", 500);
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            var result = await _reportRepository.Delete(id);
            if (result > 0)
                return Response<NoContent>.Success(204);
            else
                return Response<NoContent>.Fail("Report not found", 404);
        }
    }
}
