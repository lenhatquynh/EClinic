using System.ComponentModel.DataAnnotations;

namespace Project.IdentityService.Dtos
{
    public class SignUpDtos
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }
    }
}
