using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models
{
    public class Model
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}