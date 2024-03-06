using Project.Data.Model;

namespace Project.BlogService.Data
{
    public class Blog : MongoDBEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string CoverImage { get; set; } 
        public Author Author { get; set; }
        public bool IsActive { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public List<Guid> HashtagId { get; set; } = new List<Guid>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
