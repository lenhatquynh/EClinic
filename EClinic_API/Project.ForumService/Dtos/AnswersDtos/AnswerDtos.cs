using Project.ForumService.Data;
using Project.ForumService.Dtos.HashtagDtos;

namespace Project.ForumService.Dtos.AnswersDtos
{
    public class AnswerDtos
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public List<HashtagsDtos> HashTags { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public Author Author { get; set; }
        public int Likes { get; set; }
        public bool IsLike { get; set; }
    }
}
