using Project.Data.Model;

namespace Project.ForumService.Data
{
    public class Comment : MongoDBEntity
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Guid> LikeUserIds { get; set; } = new List<Guid>();
        public List<ReplyComment> ReplyComments { get; set; } = new List<ReplyComment>();
    }
}
