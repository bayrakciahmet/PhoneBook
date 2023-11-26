using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PhoneBook.Services.Person.Models
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UUID { get; set; }

        [BsonElement("FirstName")]
        public string? FirstName { get; set; }

        [BsonElement("LastName")]
        public string? LastName { get; set; }

        [BsonElement("Company")]
        public string? Company { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }

        [BsonIgnore] // BsonIgnore mongo db ye yansıtmıyor
        public long? ContactInfoCount { get; set; }
    }
}
