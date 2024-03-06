namespace Project.ProfileService.Data
{
    public class Relationship
    {
        public Guid RelationshipID { get; set; }
        public string RelationshipName { get; set;}
        public ICollection<HealthProfile> HealthProfiles { get; set;}
    }
}
