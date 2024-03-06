namespace Project.ForumService.Dtos.CommentsDtos
{
    public class UpdateReplyCommentDtos
    {
        public Guid ParentCommentID { get; set; }
        public Guid CommentID { get; set; }
        public string Content { get; set; }
    }
}
