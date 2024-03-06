using Project.Common.Paging;

namespace Project.IdentityService.Dtos
{
    public class SearchUserDtos : SearchBase
    {
        public string UserName { get; set; }
        public bool? Enabled { get; set; }
        public DateTime CreateTimeFrom { get; set; } = new DateTime(1, 1, 1, 0, 0, 0);
        public DateTime CreateTimeTo { get; set; } = DateTime.Now;
        public DateTime UpdateTimeFrom { get; set; } = new DateTime(1, 1, 1, 0, 0, 0);
        public DateTime UpdateTimeTo { get; set; } = DateTime.Now;
        public string Role { get; set; }
    }
}
