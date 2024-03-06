using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Repository.ServiceRepository;

namespace Project.ServiceInformationService.Handlers.ServiceHandler;

public class ToggleActiveServiceHandler : IRequestHandler<ToggleActiveServiceCommand, ObjectResult>
{
    private readonly ILogger<ToggleActiveServiceHandler> logger;
    private readonly IServiceRepository repository;
    private readonly IMapper mapper;

    public ToggleActiveServiceHandler(ILogger<ToggleActiveServiceHandler> logger, IServiceRepository repository, IMapper mapper)
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<ObjectResult> Handle(ToggleActiveServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var service = await repository.GetAsync(request.serviceID);

            if (service is null)
            {
                return ApiResponse.BadRequest("Service not found!");
            }

            service.IsActive = request.flag;
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
