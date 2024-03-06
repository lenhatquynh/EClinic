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

public class CreateServicePackageHandler : IRequestHandler<CreateServicePackageCommand, ObjectResult>
{
    private readonly ILogger<CreateServicePackageHandler> logger;
    private readonly IServicePackageRepository repository;
    private readonly IServicePackageItemRepository servicePackageItemRepository;
    private readonly IMapper mapper;
    private readonly IAmazonS3Bucket bucket;

    public CreateServicePackageHandler(ILogger<CreateServicePackageHandler> logger, IServicePackageRepository repository, IServicePackageItemRepository servicePackageItemRepository, IMapper mapper, IAmazonS3Bucket bucket)
    {
        this.logger = logger;
        this.repository = repository;
        this.servicePackageItemRepository = servicePackageItemRepository;
        this.mapper = mapper;
        this.bucket = bucket;
    }

    public async Task<ObjectResult> Handle(CreateServicePackageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.createServicePackageDTO.ServicePackageName == null
                || request.createServicePackageDTO.ServicePackageName.Trim().Count() == 0)
            {
                return ApiResponse.BadRequest("Create Service Package Error: Service Package Name must be not null!");
            }

            ServicePackage servicePackage = mapper.Map<ServicePackage>(request.createServicePackageDTO);
            servicePackage.CreatedAt = DateTime.Now;
            servicePackage.UpdatedAt = DateTime.Now;

            if (request.createServicePackageDTO.Image is not null)
            {
                var imageUrl = await bucket.UploadFileAsync(request.createServicePackageDTO.Image, FileType.Image);
                servicePackage.Image = imageUrl;
            }

            await repository.CreateAsync(servicePackage);

            foreach (var item in request.createServicePackageDTO.ServiceItemIds)
            {
                await servicePackageItemRepository.CreateAsync(
                    new ServicePackageItem { ServicePackageID = servicePackage.ServicePackageID, ServiceID = item});
            };

            return ApiResponse.OK("Create Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
