﻿using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Npgsql;
using PhoneBook.Services.Report.Dtos;
using PhoneBook.Services.Report.Repositories;
using PhoneBook.Shared.Dtos;
using System.Data;
using System.Data.Common;

namespace PhoneBook.Services.Report.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;


        public ReportService(IMapper mapper, IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
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
        public async Task<Response<NoContent>> CreateAsync(ReportCreateDto reportCreateDto)
        {
            var saveStatus = await _reportRepository.Create(_mapper.Map<Models.Report>(reportCreateDto));
            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("an error accured while adding", 500);
        }

        public async Task<Response<NoContent>> UpdateAsync(ReportUpdateDto reportUpdateDto)
        {
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
