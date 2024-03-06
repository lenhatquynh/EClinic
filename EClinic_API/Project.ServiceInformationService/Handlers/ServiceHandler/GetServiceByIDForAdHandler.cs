using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Dtos.ServiceDTOs;
using Project.ServiceInformationService.Queries;
using Project.ServiceInformationService.Repository.ServiceRepository;

namespace Project.ServiceInformationService.Handlers.ServiceHandler;

public class GetServiceByIDForAdHandler : IRequestHandler<GetServiceByIDForAdQuery, ObjectResult>
{
    private readonly IServiceRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<GetServiceByIDForAdHandler> logger;

    public GetServiceByIDForAdHandler(IServiceRepository repository, IMapper mapper, ILogger<GetServiceByIDForAdHandler> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ObjectResult> Handle(GetServiceByIDForAdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var service = await repository.GetServiceAsync(x => x.ServiceID.Equals(request.serviceID));
            if (service == null)
            {
                return ApiResponse.NotFound("Service Not Found.");
            }
            
            ServiceDTO serviceDTO = mapper.Map<ServiceDTO>(service);
            return ApiResponse.OK<ServiceDTO>(serviceDTO);
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }
    }
}
