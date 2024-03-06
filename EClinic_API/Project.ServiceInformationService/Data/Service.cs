using Project.Data.Model;

namespace Project.ServiceInformationService.Data;

public class Service : BaseEntity
{
    public Guid ServiceID { get; set; }
    public string ServiceName { get; set; }
    public double Price { get; set; }   
    public int EstimatedTime { get; set; }
    public bool IsActive { get; set; }
    // Foreign Key
    public Guid SpecializationID { get; set; }
    public Specialization Specialization { get; set; }
    public ICollection<ServicePackageItem> ServicePackageItems { get; set; }

}
