using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Dtos.ServiceDTOs;
using Project.ServiceInformationService.Dtos.ServicePackageDTOs;
using Project.ServiceInformationService.Queries;
using Project.ServiceInformationService.Repository.ServicePackageRepository;

namespace Project.ServiceInformationService.Handlers.ServicePackageHandler;

public class GetAllServicePackageForAdHandler : IRequestHandler<GetAllServicePackageForAdQuery, ObjectResult>
{
    private readonly IServicePackageRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<GetAllServicePackageForAdHandler> logger;

    public GetAllServicePackageForAdHandler(IServicePackageRepository repository, IMapper mapper, ILogger<GetAllServicePackageForAdHandler> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ObjectResult> Handle(GetAllServicePackageForAdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var servicePackages = await repository.GetAllServicePackageAsync(x => true);
            if (servicePackages == null)
            {
                return ApiResponse.NotFound("Service Package Not Found.");
            }
            PaginationResponseHeader header = new PaginationResponseHeader();
            header.TotalCount = servicePackages.Count;
            servicePackages = servicePackages
                .OrderByDescending(x => x.ServicePackageName)
                .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                .Take(request.PaginationRequestHeader.PageSize).ToList();

            header.PageIndex = request.PaginationRequestHeader.PageNumber;
            header.PageSize = request.PaginationRequestHeader.PageSize;

            request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
            List<ServicePackageDTO> servicePackageDTOs = mapper.Map<List<ServicePackageDTO>>(servicePackages);
            for (int i = 0; i < servicePackages.Count; i++)
            {
                List<ServiceDTO> services = mapper.Map<List<ServiceDTO>>(servicePackages[i].ServicePackageItems.Select(item => item.Service).ToList());
                servicePackageDTOs[i].ServiceItems = services;
            }
            return ApiResponse.OK<List<ServicePackageDTO>>(servicePackageDTOs);
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }
    }
}
