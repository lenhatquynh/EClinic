using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Dtos.ServiceDTOs;
using Project.ServiceInformationService.Queries;
using Project.ServiceInformationService.Repository.ServiceRepository;

namespace Project.ServiceInformationService.Handlers.ServiceHandler;

public class GetServiceByIDHandler : IRequestHandler<GetServiceByIDQuery, ObjectResult>
{
    private readonly IServiceRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<GetServiceByIDHandler> logger;

    public GetServiceByIDHandler(IServiceRepository repository, IMapper mapper, ILogger<GetServiceByIDHandler> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ObjectResult> Handle(GetServiceByIDQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var service = await repository.GetServiceAsync(x => x.IsActive && x.ServiceID.Equals(request.serviceID));
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
