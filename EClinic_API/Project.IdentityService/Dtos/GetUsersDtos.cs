namespace Project.IdentityService.Dtos
{
    public class GetUsersDtos
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Enabled { get; set; }
        public string RoleName { get; set; }
    }
}
