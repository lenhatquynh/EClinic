namespace Project.ServiceInformationService.Dtos.ServicePackageDTOs;

public class SearchServicePackageFilteredDTO
{
    public string SearchText { get; set; }
    public List<Guid> SpecializationIDs { get; set; }
}
