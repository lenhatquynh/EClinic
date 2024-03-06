using Project.ForumService.Data;

namespace Project.ForumService.Dtos.PostsDtos
{
    public class PostDtos
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Image { get; set; }
        public Author Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Likes { get; set; }
        public bool IsLike { get; set; } = false;
        public bool IsActive { get; set; }
    }
}
