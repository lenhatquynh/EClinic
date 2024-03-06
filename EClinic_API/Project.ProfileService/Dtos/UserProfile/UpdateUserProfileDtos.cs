using System.ComponentModel.DataAnnotations;

namespace Project.ProfileService.Dtos.UserProfile
{
    public class UpdateUserProfileDtos
    {
        [Required]
        public Guid ProfileID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public IFormFile Avatar { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string BloodType { get; set; }
        [Required]
        public float Height { get; set; }
        [Required]
        public float Weight { get; set; }
        [Required]
        public Guid RelationshipID { get; set; }
    }
}
