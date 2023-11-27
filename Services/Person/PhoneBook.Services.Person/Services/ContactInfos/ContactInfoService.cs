﻿using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;
using PhoneBook.Services.Person.Dtos.ContactInfos;
using PhoneBook.Services.Person.Dtos.Persons;
using PhoneBook.Services.Person.Models;
using PhoneBook.Services.Person.Settings.Interfaces;
using PhoneBook.Shared.Dtos;

namespace PhoneBook.Services.Person.Services.ContactInfos
{
    public class ContactInfoService : IContactInfoService
    {
       // private readonly IMongoCollection<Models.Person> _personCollection;

        private readonly IMongoCollection<Models.ContactInfo> _contactInfoCollection;

        private readonly IMapper _mapper;
        public ContactInfoService(IMapper mapper, IDatabaseSettings databaseSettings)
        {

            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            //_personCollection = database.GetCollection<Models.Person>(databaseSettings.PersonCollectionName);
            _contactInfoCollection = database.GetCollection<Models.ContactInfo>(databaseSettings.ContactInfoCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<ContactInfoDto>>> GetAllAsync()
        {
            var contactInfos = await _contactInfoCollection.Find(x => true).ToListAsync();
            if (!contactInfos.Any())
            {
                contactInfos = new List<Models.ContactInfo>();
            }

            return Response<List<ContactInfoDto>>.Success(_mapper.Map<List<ContactInfoDto>>(contactInfos), 200);
        }

        public async Task<Response<ContactInfoDto>> GetByIdAsync(string id)
        {
            var contactInfos = await _contactInfoCollection.Find<Models.ContactInfo>(x => x.UUID == id).ToListAsync();
            if (!contactInfos.Any())
            {
                contactInfos = new List<Models.ContactInfo>();
            }
            return Response<ContactInfoDto>.Success(_mapper.Map<ContactInfoDto>(contactInfos), 200);
        }

        public async Task<Response<List<ContactInfoDto>>> GetAllByPersonIdAsync(string personId)
        {
            var contactInfos = await _contactInfoCollection.Find<ContactInfo>(x => x.PersonId == personId).ToListAsync();
            if (!contactInfos.Any())
            {
                contactInfos = new List<ContactInfo>();
            }
            return Response<List<ContactInfoDto>>.Success(_mapper.Map<List<ContactInfoDto>>(contactInfos), 200);
        }

        public async Task<Response<ContactInfoDto>> CreateAsync(ContactInfoCreateDto contactInfoCreateDto)
        {
            var newContactInfo = _mapper.Map<Models.ContactInfo>(contactInfoCreateDto);
            newContactInfo.ModifiedTime = DateTime.Now;
            await _contactInfoCollection.InsertOneAsync(newContactInfo);

            return Response<ContactInfoDto>.Success(_mapper.Map<ContactInfoDto>(newContactInfo), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ContactInfoUpdateDto contactInfoUpdate)
        {
            var updateContactInfo = _mapper.Map<Models.ContactInfo>(contactInfoUpdate);
            updateContactInfo.ModifiedTime= DateTime.Now;
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
    }
}