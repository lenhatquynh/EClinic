namespace Project.ServiceInformationService.Dtos.ServiceDTOs;

public class CreateServiceDTO
{
    public string ServiceName { get; set; }
    public double Price { get; set; }   
    public int EstimatedTime { get; set; }
    public bool IsActive { get; set; }
    // Foreign Key
    public Guid SpecializationID { get; set; }
}
