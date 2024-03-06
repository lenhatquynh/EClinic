namespace Project.ProfileService.Dtos.EmployeeProfile
{
    public class GetEmployeeProfileDtos
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
        public DateTime WorkStart { get; set; }
        public DateTime? WorkEnd { get; set; } = null;
        public string Description { get; set; }
        public bool EnabledAccount { get; set; }
    }
}
