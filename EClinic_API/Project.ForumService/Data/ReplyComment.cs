using MongoDB.Bson.Serialization.Attributes;
using Project.Data.Model;

namespace Project.ForumService.Data
{
    public class ReplyComment : MongoDBEntity
    {
        [BsonId]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; }
        public Author Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Guid> LikeUserIds { get; set; } = new List<Guid>();

    }
}
