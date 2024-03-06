using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Dtos.ServiceDTOs;
using Project.ServiceInformationService.Dtos.ServicePackageDTOs;
using Project.ServiceInformationService.Queries;
using Project.ServiceInformationService.Repository.ServicePackageRepository;

namespace Project.ServiceInformationService.Handlers.ServicePackageHandler;

public class SearchServicePackageFilteredHandler : IRequestHandler<SearchServicePackageFilteredQuery, ObjectResult>
{
    private readonly IServicePackageRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<SearchServicePackageFilteredHandler> logger;

    public SearchServicePackageFilteredHandler(IServicePackageRepository repository, IMapper mapper, ILogger<SearchServicePackageFilteredHandler> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ObjectResult> Handle(SearchServicePackageFilteredQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var searchText = request.searchServicePackageFilteredDTO.SearchText?.Trim().ToLower() ?? "";

            var servicePackages = await repository.GetAllServicePackageAsync(x => x.IsActive && (x.ServicePackageName.ToLower().Contains(searchText) || x.Description.ToLower().Contains(searchText)));
            
            if (servicePackages == null)
            {
                return ApiResponse.NotFound("Service Packages Not Found.");
            }

            List<ServicePackageDTO> servicePackageDTOs = mapper.Map<List<ServicePackageDTO>>(servicePackages);
            List<ServicePackageDTO> servicePackageFilteredDTOs = new List<ServicePackageDTO>();

            for (int i = 0; i < servicePackages.Count; i++)
            {
                List<Guid> specializationIDs = servicePackages[i].ServicePackageItems.Select(item => item.Service.SpecializationID).ToList();
                // don't contain all of specializationIDs
                if (request.searchServicePackageFilteredDTO.SpecializationIDs != null &&
                    !request.searchServicePackageFilteredDTO.SpecializationIDs.All(element => specializationIDs.Contains(element)))
                {
                    servicePackageFilteredDTOs.Add(servicePackageDTOs[i]);
                } else
                {
                    List<ServiceDTO> services = mapper.Map<List<ServiceDTO>>(servicePackages[i].ServicePackageItems.Select(item => item.Service).ToList());
                    servicePackageDTOs[i].ServiceItems = services;
                }
            }

            servicePackageDTOs.RemoveAll(element => servicePackageFilteredDTOs.Contains(element));

            PaginationResponseHeader header = new PaginationResponseHeader();
            header.TotalCount = servicePackageDTOs.Count;
            servicePackageDTOs = servicePackageDTOs
                .OrderByDescending(x => x.ServicePackageName)
                .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                .Take(request.PaginationRequestHeader.PageSize).ToList();

            header.PageIndex = request.PaginationRequestHeader.PageNumber;
            header.PageSize = request.PaginationRequestHeader.PageSize;

            request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
            
            return ApiResponse.OK<List<ServicePackageDTO>>(servicePackageDTOs);
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }
    }
}
