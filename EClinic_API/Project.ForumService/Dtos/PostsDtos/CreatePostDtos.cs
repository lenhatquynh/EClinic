using Project.ForumService.Data;

namespace Project.ForumService.Dtos.PostsDtos
{
    public class CreatePostDtos
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
