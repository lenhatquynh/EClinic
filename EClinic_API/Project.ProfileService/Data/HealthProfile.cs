namespace Project.ProfileService.Data
{
    public class HealthProfile
    {
        public Guid ProfileID { get; set; }
        public string BloodType { get; set; }
        public float Height { get; set; } = 0;
        public float Weight { get; set; } = 0;
        public Guid RelationshipID { get; set; }
        public Relationship Relationship { get; set; }
        public Profile Profile { get; set; }
    }
}
