using Project.ProfileService.Data;

namespace Project.ProfileService.Dtos.UserProfile
{
    public class UserProfileDtos
    {
        public Guid ProfileID { get; set; }
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BloodType { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public Guid RelationshipID { get; set; }
        public string RelationshipName { get; set; }
    }
}
