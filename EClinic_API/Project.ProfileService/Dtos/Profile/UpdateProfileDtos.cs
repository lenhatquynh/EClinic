namespace Project.ProfileService.Dtos.Profile
{
    public class UpdateProfileDtos
    {
        public Guid ProfileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Avatar { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
