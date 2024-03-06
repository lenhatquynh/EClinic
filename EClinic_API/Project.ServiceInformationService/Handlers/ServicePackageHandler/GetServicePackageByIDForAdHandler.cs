using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Dtos.ServiceDTOs;
using Project.ServiceInformationService.Dtos.ServicePackageDTOs;
using Project.ServiceInformationService.Queries;
using Project.ServiceInformationService.Repository.ServicePackageRepository;

namespace Project.ServiceInformationService.Handlers.ServicePackageHandler;

public class GetServicePackageByIDForAdHandler : IRequestHandler<GetServicePackageByIDForAdQuery, ObjectResult>
{
    private readonly IServicePackageRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<GetServicePackageByIDForAdHandler> logger;

    public GetServicePackageByIDForAdHandler(IServicePackageRepository repository, IMapper mapper, ILogger<GetServicePackageByIDForAdHandler> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ObjectResult> Handle(GetServicePackageByIDForAdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var servicePackage = await repository.GetServicePackageAsync(x => x.ServicePackageID.Equals(request.servicePackageID));
            if (servicePackage == null)
            {
                return ApiResponse.NotFound("Service Package Not Found.");
            }
            
            ServicePackageDTO servicePackageDTO = mapper.Map<ServicePackageDTO>(servicePackage);
            List<ServiceDTO> services = mapper.Map<List<ServiceDTO>>(servicePackage.ServicePackageItems.Select(item => item.Service).ToList());
            servicePackageDTO.ServiceItems = services;

            return ApiResponse.OK<ServicePackageDTO>(servicePackageDTO);
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }
    }
}
