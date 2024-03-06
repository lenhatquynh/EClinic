using Project.ForumService.Data;

namespace Project.ForumService.Dtos.CommentsDtos
{
    public class CreateReplyCommentDtos
    {
        public Guid ParentCommentID { get; set; }
        public string Content { get; set; }
    }
}
