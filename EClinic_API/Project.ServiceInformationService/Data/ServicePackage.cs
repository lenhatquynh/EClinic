using Project.Data.Model;

namespace Project.ServiceInformationService.Data;

public class ServicePackage : BaseEntity
{
    public Guid ServicePackageID { get; set; }
    public string ServicePackageName { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public double Price { get; set; } = 0;
    public double Discount { get; set; }
    public int TotalOrder { get; set; } = 0;
    public int EstimatedTime { get; set; }
    public bool IsActive { get; set; }

    // Foreign Key
    public ICollection<ServicePackageItem> ServicePackageItems { get; set; }

}
