namespace Project.ServiceInformationService.Data;

public class Specialization 
{
    public Guid SpecializationID { get; set; }
    public string SpecializationName { get; set; }

    // Foreign Key
    public ICollection<Service> Services { get; set; }

}
