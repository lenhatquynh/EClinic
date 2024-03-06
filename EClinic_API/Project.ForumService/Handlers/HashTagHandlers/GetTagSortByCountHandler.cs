using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Data;
using Project.ForumService.Dtos.HashtagDtos;
using Project.ForumService.Queries;

namespace Project.ForumService.Handlers.HashTagHandlers
{
    public class GetTagSortByCountHandler:IRequestHandler<GetTagSortByCountQuery,ObjectResult>
    {
        private readonly IMongoDBRepository<Hashtag> repository;
        private readonly IMapper mapper;
        private readonly ILogger<GetTagSortByCountHandler> logger;

        public GetTagSortByCountHandler(IMongoDBRepository<Hashtag> repository, IMapper mapper, ILogger<GetTagSortByCountHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetTagSortByCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tags = await repository.GetAllAsync();
                if (tags == null)
                {
                    return ApiResponse.NotFound("HashTag Not Found.");
                }
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = tags.Count;
                tags = tags
                    .OrderByDescending(x => x.Count)
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();

                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;

                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
                List<HashtagsDtos> tagDtos = mapper.Map<List<HashtagsDtos>>(tags);
                return ApiResponse.OK<List<HashtagsDtos>>(tagDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
