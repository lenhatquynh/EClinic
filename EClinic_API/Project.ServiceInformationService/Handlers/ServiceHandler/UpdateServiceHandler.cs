using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Repository.ServiceRepository;

namespace Project.ServiceInformationService.Handlers.ServiceHandler;

public class UpdateServiceHandler : IRequestHandler<UpdateServiceCommand, ObjectResult>
{
    private readonly ILogger<UpdateServiceHandler> logger;
    private readonly IServiceRepository repository;
    private readonly IMapper mapper;

    public UpdateServiceHandler(ILogger<UpdateServiceHandler> logger, IServiceRepository repository, IMapper mapper)
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<ObjectResult> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.updateServiceDTO.ServiceName == null
                || request.updateServiceDTO.ServiceName.Trim().Count() == 0)
            {
                return ApiResponse.BadRequest("Update Service Error: Service Name must be not null!");
            }

            var service = await repository.GetAsync(request.updateServiceDTO.ServiceID);

            if (service is null)
            {
                return ApiResponse.BadRequest("Service not found!");
            }

            service = mapper.Map<Data.Service>(request.updateServiceDTO);
            service.UpdatedAt = DateTime.Now;

            await repository.UpdateAsync(service);

            return ApiResponse.OK("Update Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
