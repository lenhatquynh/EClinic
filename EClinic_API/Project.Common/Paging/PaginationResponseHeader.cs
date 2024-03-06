namespace Project.Common.Paging
{
    public class PaginationResponseHeader
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < TotalPages;
    }
}
