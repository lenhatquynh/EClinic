using Project.Data.Model;

namespace Project.IdentityService.Data
{
    public class User : BaseEntity
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Enabled { get; set; } = true;
        public string RoleID { get; set; }
        public Role Role { get; set; }
    }
}
