using Project.IdentityService.Data;
using Project.IdentityService.Protos;

namespace Project.IdentityService.Dtos
{
    public class DataCodeDtos
    {
        public User User { get; set; }
        public string Code { get; set; }
        public CreateProfileRequest CreateProfileRequest { get; set; }
    }
}
