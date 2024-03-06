using Project.ServiceInformationService.Data;

namespace Project.ServiceInformationService.Dtos.ServiceDTOs;

public class UpdateServiceDTO
{
    public Guid ServiceID { get; set; }
    public string ServiceName { get; set; }
    public double Price { get; set; }
    public int EstimatedTime { get; set; }
    public bool IsActive { get; set; }
    // Foreign Key
    public Guid SpecializationID { get; set; }
}
