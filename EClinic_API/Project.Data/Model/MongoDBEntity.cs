using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Data.Model
{
    public interface MongoDBEntity
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
