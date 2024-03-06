using Newtonsoft.Json;

namespace Project.IdentityService.Dtos
{
    public class ProviderAccountReqDtos
    {
        public Guid UserID { get; set; }
        public string Email { get; set; }
    }
}
