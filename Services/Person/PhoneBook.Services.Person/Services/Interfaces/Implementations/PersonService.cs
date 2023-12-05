using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;
using PhoneBook.Services.Person.Dtos.Persons;
using PhoneBook.Services.Person.Settings.Interfaces;
using PhoneBook.Shared.Dtos;

namespace PhoneBook.Services.Person.Services.Interfaces.Implementations
{
    public class PersonService : IPersonService
    {
        public readonly IMongoCollection<Models.Person> _personCollection;

        public readonly IMongoCollection<Models.ContactInfo> _contactInfoCollection;

        private readonly IMapper _mapper;

        public PersonService(IMapper mapper, IDatabaseSettings databaseSettings)
        {

            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _personCollection = database.GetCollection<Models.Person>(databaseSettings.PersonCollectionName);
            _contactInfoCollection = database.GetCollection<Models.ContactInfo>(databaseSettings.ContactInfoCollectionName);
            _mapper = mapper;
        }


        public async Task<Response<List<PersonDto>>> GetAllAsync()
        {
            var persons = await _personCollection.Find(x => true).ToListAsync();
            if (persons.Any())
            {
                foreach (var person in persons)
                {
                    person.ContactInfoCount = await _contactInfoCollection.CountDocumentsAsync(x => x.PersonId == person.UUID);
                }
            }
            else
                persons = new List<Models.Person>();

            return Response<List<PersonDto>>.Success(_mapper.Map<List<PersonDto>>(persons), 200);
        }
        public async Task<Response<PersonDto>> GetByIdAsync(string id)
        {
            var persons = await _personCollection.Find(x => x.UUID == id).FirstOrDefaultAsync();
            if (persons == null)
            {
                return Response<PersonDto>.Fail("person not found", 404);
            }
            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(persons), 200);
        }

        public async Task<Response<PersonDto>> CreateAsync(PersonCreateDto personCreateDto)
        {
            var newPerson = _mapper.Map<Models.Person>(personCreateDto);
            newPerson.CreatedTime = DateTime.Now;
            await _personCollection.InsertOneAsync(newPerson);

            return Response<PersonDto>.Success(_mapper.Map<PersonDto>(newPerson), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(PersonUpdateDto personUpdateDto)
        {
            var updatePerson = _mapper.Map<Models.Person>(personUpdateDto);

            var result = await _personCollection.FindOneAndReplaceAsync(x => x.UUID == personUpdateDto.UUID, updatePerson);
            if (result == null)
            {
                return Response<NoContent>.Fail("Person not found", 404);
            }

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _personCollection.DeleteOneAsync(x => x.UUID == id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
                return Response<NoContent>.Fail("Person not found", 404);
        }

    }
}
