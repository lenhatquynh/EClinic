using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Repository.ServicePackageRepository;
using Project.ServiceInformationService.Repository.ServiceRepository;
using Project.ServiceInformationService.Repository.SpecializationRepository;

namespace Project.ServiceInformationService.Handlers.ServicePackageHandler;

public class DeleteServicePackageHandler : IRequestHandler<DeleteServicePackageCommand, ObjectResult>
{
    private readonly ILogger<DeleteServicePackageHandler> logger;
    private readonly IServicePackageRepository repository;
    private readonly IMapper mapper;

    public DeleteServicePackageHandler(ILogger<DeleteServicePackageHandler> logger, IServicePackageRepository repository, ISpecializationRepository specializationRepository, IMapper mapper)
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<ObjectResult> Handle(DeleteServicePackageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var servicePackage = await repository.GetAsync(request.deleteServicePackageID);

            if (servicePackage is null)
            {
                return ApiResponse.BadRequest("Service Package not found!");
            }

            await repository.DeleteAsync(servicePackage);

            return ApiResponse.OK("Delete Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
