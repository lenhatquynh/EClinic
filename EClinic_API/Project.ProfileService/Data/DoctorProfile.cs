namespace Project.ProfileService.Data
{
    public class DoctorProfile
    {
        public Guid ProfileID { get; set; }
        public bool IsActive { get; set; } = false;
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public DateTime WorkStart { get; set; }
        public DateTime? WorkEnd { get; set; }
        public float Price { get; set; } = 0;
        public Guid SpecializationID { get; set; }
        public Profile Profile { get; set; }
    }
}
