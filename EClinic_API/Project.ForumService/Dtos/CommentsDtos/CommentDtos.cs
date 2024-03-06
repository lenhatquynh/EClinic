using Newtonsoft.Json;
using Project.ForumService.Data;

namespace Project.ForumService.Dtos.CommentsDtos
{
    public class CommentDtos
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Author Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public List<Guid> LikeUserIds { get; set; }
        public int Likes { get; set; } = 0;
        public bool IsLike { get; set; }
        public List<ReplyCommentDtos> ReplyCommentDtos { get; set; }
    }
}
