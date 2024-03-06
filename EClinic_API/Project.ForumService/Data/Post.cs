using Project.Data.Model;

namespace Project.ForumService.Data
{
    public class Post : MongoDBEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Image { get; set; } = new List<string>();
        public Author Author { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Guid> LikeUserIds { get; set; } = new List<Guid>();

    }
}
