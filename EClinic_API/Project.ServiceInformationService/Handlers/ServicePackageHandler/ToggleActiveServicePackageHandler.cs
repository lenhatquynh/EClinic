using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Repository.ServicePackageRepository;

namespace Project.ServiceInformationService.Handlers.ServicePackageHandler;

public class ToggleActiveServicePackageHandler : IRequestHandler<ToggleActiveServicePackageCommand, ObjectResult>
{
    private readonly ILogger<ToggleActiveServicePackageHandler> logger;
    private readonly IServicePackageRepository repository;

    public ToggleActiveServicePackageHandler(ILogger<ToggleActiveServicePackageHandler> logger, IServicePackageRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    public async Task<ObjectResult> Handle(ToggleActiveServicePackageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var servicePackage = await repository.GetAsync(request.servicePackageID);

            if (servicePackage is null)
            {
                return ApiResponse.BadRequest("Service Package not found!");
            }

            servicePackage.IsActive = request.flag;
            servicePackage.UpdatedAt = DateTime.Now;

            await repository.UpdateAsync(servicePackage);

            return ApiResponse.OK("Update Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
