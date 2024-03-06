using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Data;
using Project.ForumService.Dtos.PostsDtos;
using Project.ForumService.Queries;

namespace Project.ForumService.Handlers.PostHandlers
{
    public class GetPostNotActiveHandler : IRequestHandler<GetPostNotActiveQuery, ObjectResult>
    {
        private readonly IMongoDBRepository<Post> repository;
        private readonly IMapper mapper;
        private readonly ILogger<GetPostNotActiveHandler> logger;

        public GetPostNotActiveHandler(IMongoDBRepository<Post> repository, IMapper mapper, ILogger<GetPostNotActiveHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<ObjectResult> Handle(GetPostNotActiveQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var text = "";
                if (request.SearchText != null) { text = request.SearchText; }
                var posts = await repository.GetAllAsync(x => x.IsActive == false &&(x.Title.Contains(text)
                                                        || x.Content.Contains(text)
                                                        || x.Author.FirstName.Contains(text)
                                                        || x.Author.LastName.Contains(text)));
                if (posts == null)
                {
                    return ApiResponse.NotFound("Post Not Found.");
                }
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = posts.Count;
                posts = posts
                    .OrderBy(x => x.CreatedAt)
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();

                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;

                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
                List<PostDtos> postDtos = mapper.Map<List<PostDtos>>(posts);
                return ApiResponse.OK<List<PostDtos>>(postDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
