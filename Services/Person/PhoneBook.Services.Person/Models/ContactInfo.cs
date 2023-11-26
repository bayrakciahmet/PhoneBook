using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PhoneBook.Services.Person.Models
{
    public class ContactInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UUID { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? PersonId { get; set; }

        [BsonElement("InfoType")]
        public string? InfoType { get; set; }

        [BsonElement("InfoContent")]
        public string? InfoContent { get; set; }

        [BsonElement("ModifiedTime")]
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime ModifiedTime { get; set; }
    }
}
