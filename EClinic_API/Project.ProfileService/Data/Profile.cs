namespace Project.ProfileService.Data
{
    public class Profile
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
        public EmployeeProfile EmployeeProfile { get; set; }
        public DoctorProfile DoctorProfile { get; set; }
        public HealthProfile HealthProfile { get; set; }
    }
}
