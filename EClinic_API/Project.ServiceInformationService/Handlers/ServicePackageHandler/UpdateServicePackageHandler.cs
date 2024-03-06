using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Enum;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Repository.ServicePackageItemRepository;
using Project.ServiceInformationService.Repository.ServicePackageRepository;
using Project.ServiceInformationService.Repository.ServiceRepository;

namespace Project.ServiceInformationService.Handlers.ServicePackageHandler;

public class UpdateServicePackageHandler : IRequestHandler<UpdateServicePackageCommand, ObjectResult>
{
    private readonly ILogger<UpdateServicePackageHandler> logger;
    private readonly IServicePackageRepository repository;
    private readonly IServicePackageItemRepository servicePackageItemRepository;
    private readonly IMapper mapper;
    private readonly IAmazonS3Bucket bucket;

    public UpdateServicePackageHandler(ILogger<UpdateServicePackageHandler> logger, IServicePackageRepository repository, 
                            IServicePackageItemRepository servicePackageItemRepository, IMapper mapper, IAmazonS3Bucket bucket)
    {
        this.logger = logger;
        this.repository = repository;
        this.servicePackageItemRepository = servicePackageItemRepository;
        this.mapper = mapper;
        this.bucket = bucket;
    }

    public async Task<ObjectResult> Handle(UpdateServicePackageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.updateServicePackageDTO.ServicePackageName == null
                || request.updateServicePackageDTO.ServicePackageName.Trim().Count() == 0)
            {
                return ApiResponse.BadRequest("Update Service Package Error: Service Package Name must be not null!");
            }

            var servicePackage = await repository.GetAsync(request.updateServicePackageDTO.ServicePackageID);

            if (servicePackage is null)
            {
                return ApiResponse.BadRequest("Service Package not found!");
            }

            servicePackage.ServicePackageName = request.updateServicePackageDTO.ServicePackageName;
            servicePackage.Description = request.updateServicePackageDTO.Description;
            servicePackage.Price = request.updateServicePackageDTO.Price;
            servicePackage.Discount = request.updateServicePackageDTO.Discount;
            servicePackage.EstimatedTime = request.updateServicePackageDTO.EstimatedTime;
            servicePackage.IsActive = request.updateServicePackageDTO.IsActive;

            servicePackage.UpdatedAt = DateTime.Now;

            if (request.updateServicePackageDTO.Image is not null)
            {
                var imageUrl = await bucket.UploadFileAsync(request.updateServicePackageDTO.Image, FileType.Image);
                servicePackage.Image = imageUrl;
            }

            await repository.UpdateAsync(servicePackage);

            var servicePackageItems = await servicePackageItemRepository.GetAllAsync(x => x.ServicePackageID.Equals(servicePackage.ServicePackageID));
            if (servicePackageItems is not null)
            {
                await servicePackageItemRepository.DeleteRangeAsync(servicePackageItems);
            }

            foreach (var item in request.updateServicePackageDTO.ServiceItemIds)
            {
                await servicePackageItemRepository.CreateAsync(
                    new ServicePackageItem { ServicePackageID = servicePackage.ServicePackageID, ServiceID = item });
            };

            return ApiResponse.OK("Update Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
