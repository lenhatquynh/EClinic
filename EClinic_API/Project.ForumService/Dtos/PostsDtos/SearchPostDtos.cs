namespace Project.ForumService.Dtos.PostsDtos
{
    public class SearchPostDtos
    {
        public string SearchText { get; set; }
        public List<Guid> Tags { get; set; }
    }
}
