﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;
using PhoneBook.Services.Person.Dtos.ContactInfos;
using PhoneBook.Services.Person.Dtos.Persons;
using PhoneBook.Services.Person.Models;
using PhoneBook.Services.Person.Settings.Interfaces;
using PhoneBook.Services.Person.Validators.ContactInfos;
using PhoneBook.Shared.Dtos;

namespace PhoneBook.Services.Person.Services.Interfaces.Implementations
{
    public class ContactInfoService : IContactInfoService
    {
        // private readonly IMongoCollection<Models.Person> _personCollection;

        private readonly IMongoCollection<ContactInfo> _contactInfoCollection;

        private readonly IMapper _mapper;
        private IValidator<ContactInfoCreateDto> _contactInfoCreateDtoValidator;
        private IValidator<ContactInfoUpdateDto> _contactInfoUpdateDtoValidator;
        public ContactInfoService(IMapper mapper, IDatabaseSettings databaseSettings, 
            IValidator<ContactInfoUpdateDto> contactInfoUpdateDtoValidator, 
            IValidator<ContactInfoCreateDto> contactInfoCreateDtoValidator)
        {

            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            //_personCollection = database.GetCollection<Models.Person>(databaseSettings.PersonCollectionName);
            _contactInfoCollection = database.GetCollection<ContactInfo>(databaseSettings.ContactInfoCollectionName);
            _mapper = mapper;
            _contactInfoUpdateDtoValidator = contactInfoUpdateDtoValidator;
            _contactInfoCreateDtoValidator = contactInfoCreateDtoValidator;
        }

        public async Task<Response<List<ContactInfoDto>>> GetAllAsync()
        {
            var contactInfos = await _contactInfoCollection.Find(x => true).ToListAsync();
            if (!contactInfos.Any())
            {
                contactInfos = new List<ContactInfo>();
            }

            return Response<List<ContactInfoDto>>.Success(_mapper.Map<List<ContactInfoDto>>(contactInfos), 200);
        }

        public async Task<Response<ContactInfoDto>> GetByIdAsync(string id)
        {
            var contactInfos = await _contactInfoCollection.Find(x => x.UUID == id).FirstOrDefaultAsync();
            if (contactInfos == null)
            {
                contactInfos = new ContactInfo();
            }
            return Response<ContactInfoDto>.Success(_mapper.Map<ContactInfoDto>(contactInfos), 200);
        }

        public async Task<Response<List<ContactInfoDto>>> GetAllByPersonIdAsync(string personId)
        {
            var contactInfos = await _contactInfoCollection.Find(x => x.PersonId == personId).ToListAsync();
            if (!contactInfos.Any())
            {
                contactInfos = new List<ContactInfo>();
            }
            return Response<List<ContactInfoDto>>.Success(_mapper.Map<List<ContactInfoDto>>(contactInfos), 200);
        }

        public async Task<Response<ContactInfoDto>> CreateAsync(ContactInfoCreateDto contactInfoCreateDto)
        {
            var validationResult = await _contactInfoCreateDtoValidator.ValidateAsync(contactInfoCreateDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Response<ContactInfoDto>.Fail(errors, 400);
            }
            var newContactInfo = _mapper.Map<ContactInfo>(contactInfoCreateDto);
            newContactInfo.ModifiedTime = DateTime.Now;
            await _contactInfoCollection.InsertOneAsync(newContactInfo);
            return Response<ContactInfoDto>.Success(_mapper.Map<ContactInfoDto>(newContactInfo), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ContactInfoUpdateDto contactInfoUpdate)
        {
            var validationResult = await _contactInfoUpdateDtoValidator.ValidateAsync(contactInfoUpdate);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return Response<NoContent>.Fail(errors, 400);
            }
            var updateContactInfo = _mapper.Map<ContactInfo>(contactInfoUpdate);
            updateContactInfo.ModifiedTime = DateTime.Now;
            var result = await _contactInfoCollection.FindOneAndReplaceAsync(x => x.UUID == contactInfoUpdate.UUID, updateContactInfo);
            if (result == null)
            {
                return Response<NoContent>.Fail("Contact Info not found", 404);
            }

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _contactInfoCollection.DeleteOneAsync(x => x.UUID == id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
                return Response<NoContent>.Fail("Contact Info not found", 404);
        }

        public async Task<Response<List<Dtos.Report.ReportDto>>> GetReport()
        {
            var distinctLocations = await _contactInfoCollection
            .Distinct<string>("InfoContent", Builders<ContactInfo>.Filter.Eq("InfoType", "Konum"))
            .ToListAsync();
            List<Dtos.Report.ReportDto> reportdtos = new List<Dtos.Report.ReportDto>();
            foreach (var location in distinctLocations)
            {
                var locationQuery = Builders<ContactInfo>.Filter.Eq("InfoContent", location);
                var locationResults = await _contactInfoCollection.Find(locationQuery).ToListAsync();
                var personIds = locationResults.ConvertAll(info => info.PersonId);
                var personCount = personIds.Count;
                var phoneQuery = Builders<ContactInfo>.Filter.And(
                    Builders<ContactInfo>.Filter.In("PersonId", personIds),
                    Builders<ContactInfo>.Filter.Eq("InfoType", "Telefon")
                );
                var phoneCount = await _contactInfoCollection.CountDocumentsAsync(phoneQuery);
                reportdtos.Add(new Dtos.Report.ReportDto()
                {
                    LocationName = location,
                    PhoneNumberCount = phoneCount,
                    PersonCount = personCount
                });
            }
            return Response<List<Dtos.Report.ReportDto>>.Success(_mapper.Map<List<Dtos.Report.ReportDto>>(reportdtos), 200);
        }
    }
}
