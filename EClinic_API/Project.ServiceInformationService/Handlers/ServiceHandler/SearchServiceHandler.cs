﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Dtos.ServiceDTOs;
using Project.ServiceInformationService.Queries;
using Project.ServiceInformationService.Repository.ServiceRepository;

namespace Project.ServiceInformationService.Handlers.ServiceHandler;

public class SearchServiceHandler : IRequestHandler<SearchServiceQuery, ObjectResult>
{
    private readonly IServiceRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<SearchServiceHandler> logger;

    public SearchServiceHandler(IServiceRepository repository, IMapper mapper, ILogger<SearchServiceHandler> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ObjectResult> Handle(SearchServiceQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var searchText = request.searchServiceDTO.SearchText?.Trim().ToLower() ?? "";
            var specializationID = request.searchServiceDTO.SpecializationID;
           
            var services = await repository.GetAllServiceAsync(x => x.IsActive && x.ServiceName.ToLower().Contains(searchText) && (specializationID.Equals(Guid.Empty) || x.SpecializationID.Equals(specializationID)));
            if (services == null)
            {
                return ApiResponse.NotFound("Service Not Found.");
            }
            PaginationResponseHeader header = new PaginationResponseHeader();
            header.TotalCount = services.Count;
            services = services
                .OrderByDescending(x => x.ServiceName)
                .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                .Take(request.PaginationRequestHeader.PageSize).ToList();

            header.PageIndex = request.PaginationRequestHeader.PageNumber;
            header.PageSize = request.PaginationRequestHeader.PageSize;

            request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
            List<ServiceDTO> serviceDTOs = mapper.Map<List<ServiceDTO>>(services);
            return ApiResponse.OK<List<ServiceDTO>>(serviceDTOs);
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }
    }
}
