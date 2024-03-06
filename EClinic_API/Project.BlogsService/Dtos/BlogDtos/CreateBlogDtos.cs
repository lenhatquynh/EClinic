namespace Project.BlogService.Dtos.BlogsDtos;

public class CreateBlogDtos
{
    public string Title { get; set; }
    public string Content { get; set; }
    public IFormFile CoverImage { get; set; }
    public bool IsActive { get; set; }
    public string MetaTitle { get; set; }
    public string MetaDescription { get; set; }
    public string MetaKeywords { get; set; }
    public List<Guid> HashtagId { get; set; } = new List<Guid>();
}
