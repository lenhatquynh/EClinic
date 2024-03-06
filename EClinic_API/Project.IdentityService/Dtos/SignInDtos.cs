using System.ComponentModel.DataAnnotations;

namespace Project.IdentityService.Dtos
{
    public class SignInDtos
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
