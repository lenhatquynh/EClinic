namespace Project.BlogService.Dtos.BlogDtos;

public class SearchBlogDtos
{
    public string SearchText { get; set; }
    public List<Guid> Tags { get; set; }
}
