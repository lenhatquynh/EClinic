namespace Project.ServiceInformationService.Data;

public class ServicePackageItem
{
    public Guid ServicePackageID { get; set; }
    public Guid ServiceID { get; set; }

    // Foreign Key
    public ServicePackage ServicePackage { get; set; }
    public Service Service { get; set; }

}
