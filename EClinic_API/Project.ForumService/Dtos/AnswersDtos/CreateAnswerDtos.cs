using Project.ForumService.Data;

namespace Project.ForumService.Dtos.AnswersDtos
{
    public class CreateAnswerDtos
    {
        public Guid PostID { get; set; }
        public string Content { get; set; }
        public List<Guid> Tags { get; set; }
    }
}
