namespace Project.ProfileService.Data
{
    public class EmployeeProfile
    {
        public Guid ProfileID { get; set; }
        public DateTime WorkStart { get; set; }
        public DateTime? WorkEnd { get; set; }
        public string Description { get; set; }
        public Profile Profile { get; set; }
    }
}
