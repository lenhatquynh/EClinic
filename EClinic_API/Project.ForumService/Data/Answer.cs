using Project.Data.Model;

namespace Project.ForumService.Data
{
    public class Answer : MongoDBEntity
    {
        public Guid Id { get; set; }
        public Guid PostID { get; set; }
        public string Content { get; set; }
        public List<Guid> Tags { get; set; }= new List<Guid>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Author Author { get; set; }
        public List<Guid> LikeUserIds { get; set; } = new List<Guid>();

    }
}
