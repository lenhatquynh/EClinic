using AutoMapper;
using MediatR;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Repository.ServicePackageRepository;

namespace Project.ServiceInformationService.Handlers.ServicePackageHandler;

public class IncreaseOrderHandler : IRequestHandler<IncreaseOrderCommand, bool>
{
    private readonly ILogger<IncreaseOrderHandler> logger;
    private readonly IServicePackageRepository repository;

    public IncreaseOrderHandler(ILogger<IncreaseOrderHandler> logger, IServicePackageRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    public async Task<bool> Handle(IncreaseOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var servicePackage = await repository.GetAsync(request.servicePackageID);

            if (servicePackage is null)
            {
                return false;
            }

            servicePackage.TotalOrder++;
            servicePackage.UpdatedAt = DateTime.Now;



            return await repository.UpdateAsync(servicePackage);
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return false;
        }

    }
}
