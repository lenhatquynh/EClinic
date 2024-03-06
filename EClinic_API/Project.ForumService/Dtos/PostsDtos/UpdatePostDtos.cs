using Project.ForumService.Data;

namespace Project.ForumService.Dtos.PostsDtos
{
    public class UpdatePostDtos
    {
        public Guid PostID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
