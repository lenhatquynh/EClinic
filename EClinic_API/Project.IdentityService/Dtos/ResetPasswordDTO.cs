using System.ComponentModel.DataAnnotations;

namespace Project.IdentityService.Dtos
{
    public class ResetPasswordDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string NewPassword { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
    }
}
