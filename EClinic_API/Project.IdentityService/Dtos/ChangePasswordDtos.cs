using System.ComponentModel.DataAnnotations;

namespace Project.IdentityService.Dtos
{
    public class ChangePasswordDtos
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string NewPassword { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
    }
}
