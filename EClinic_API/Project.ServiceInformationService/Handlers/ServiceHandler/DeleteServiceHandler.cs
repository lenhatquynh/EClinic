using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Repository.ServiceRepository;
using Project.ServiceInformationService.Repository.SpecializationRepository;

namespace Project.ServiceInformationService.Handlers.ServiceHandler;

public class DeleteServiceHandler : IRequestHandler<DeleteServiceCommand, ObjectResult>
{
    private readonly ILogger<DeleteServiceHandler> logger;
    private readonly IServiceRepository repository;
    private readonly IMapper mapper;

    public DeleteServiceHandler(ILogger<DeleteServiceHandler> logger, IServiceRepository repository, ISpecializationRepository specializationRepository, IMapper mapper)
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<ObjectResult> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var service = await repository.GetAsync(request.deleteServiceID);

            if (service is null)
            {
                return ApiResponse.BadRequest("Service not found!");
            }

            await repository.DeleteAsync(service);

            return ApiResponse.OK("Delete Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
