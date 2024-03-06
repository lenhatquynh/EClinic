namespace Project.ProfileService.Dtos.DoctorProfile
{
    public class SearchDoctorDtos
    {
        public Guid? SpecializationID { get; set; }
        public string SearchText { get; set; } = null;
        public float StartPrice { get; set; } = 0;
        public float EndPrice { get; set; } = 0;
    }
}
