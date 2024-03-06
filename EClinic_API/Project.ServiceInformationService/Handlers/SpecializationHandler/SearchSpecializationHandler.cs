using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Dtos.SpecializationDTOs;
using Project.ServiceInformationService.Queries;
using Project.ServiceInformationService.Repository.SpecializationRepository;

namespace Project.ServiceInformationService.Handlers.SpecializationHandler;

public class SearchSpecializationHandler : IRequestHandler<SearchSpecializationQuery, ObjectResult>
{
    private readonly ISpecializationRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<SearchSpecializationHandler> logger;

    public SearchSpecializationHandler(ISpecializationRepository repository, IMapper mapper, ILogger<SearchSpecializationHandler> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<ObjectResult> Handle(SearchSpecializationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var searchText = request.searchSpecializationDTO.SearchText?.Trim().ToLower() ?? "";
            var specializations = await repository.GetAllAsync(x => x.SpecializationName.ToLower().Contains(searchText));
            if (specializations == null)
            {
                return ApiResponse.NotFound("Specializations Not Found.");
            }
            PaginationResponseHeader header = new PaginationResponseHeader();
            header.TotalCount = specializations.Count;
            specializations = specializations
                .OrderByDescending(x => x.SpecializationName)
                .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                .Take(request.PaginationRequestHeader.PageSize).ToList();

            header.PageIndex = request.PaginationRequestHeader.PageNumber;
            header.PageSize = request.PaginationRequestHeader.PageSize;

            request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
            List<SpecializationDTO> specializationDTOs = mapper.Map<List<SpecializationDTO>>(specializations);
            return ApiResponse.OK<List<SpecializationDTO>>(specializationDTOs);
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }
    }
}
