using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.ServiceInformationService.Dtos.ServiceDTOs;
using Project.ServiceInformationService.Dtos.ServicePackageDTOs;
using Project.ServiceInformationService.Dtos.SpecializationDTOs;

namespace Project.ServiceInformationService.Commands
{
    #region Specialization
    public record CreateSpecializationCommand(CreateSpecializationDTO createSpecializationDTO) : IRequest<ObjectResult>;
    public record UpdateSpecializationCommand(UpdateSpecializationDTO updateSpecializationDTO) : IRequest<ObjectResult>;
    public record DeleteSpecializationCommand(DeleteSpecializationDTO deleteSpecializationDTO) : IRequest<ObjectResult>;
    #endregion
    #region Service
    public record CreateServiceCommand(CreateServiceDTO createServiceDTO) : IRequest<ObjectResult>;
    public record UpdateServiceCommand(UpdateServiceDTO updateServiceDTO) : IRequest<ObjectResult>;
    public record DeleteServiceCommand(Guid deleteServiceID) : IRequest<ObjectResult>;
    public record ToggleActiveServiceCommand(Guid serviceID, bool flag) : IRequest<ObjectResult>;
    #endregion
    #region ServicePackage
    public record CreateServicePackageCommand(CreateServicePackageDTO createServicePackageDTO) : IRequest<ObjectResult>;
    public record UpdateServicePackageCommand(UpdateServicePackageDTO updateServicePackageDTO) : IRequest<ObjectResult>;
    public record DeleteServicePackageCommand(Guid deleteServicePackageID) : IRequest<ObjectResult>;
    public record ToggleActiveServicePackageCommand(Guid servicePackageID, bool flag) : IRequest<ObjectResult>;
    public record IncreaseOrderCommand(Guid servicePackageID) : IRequest<bool>;
    #endregion
}