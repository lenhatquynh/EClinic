using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Dtos.ServiceDTOs;

namespace Project.ServiceInformationService.Dtos.ServicePackageDTOs;

public class ServicePackageDTO
{
    public Guid ServicePackageID { get; set; }
    public string ServicePackageName { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }
    public int TotalOrder { get; set; }
    public int EstimatedTime { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Foreign Key
    public List<ServiceDTO> ServiceItems { get; set; }
}
