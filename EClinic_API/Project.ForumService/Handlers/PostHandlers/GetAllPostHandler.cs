using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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
    public class GetAllPostHandler : IRequestHandler<GetAllPostQuery, ObjectResult>
    {
        private readonly IMongoDBRepository<Post> repository;
        private readonly IMapper mapper;
        private readonly ILogger<GetAllPostHandler> logger;
        public GetAllPostHandler(IMongoDBRepository<Post> repository, IMapper mapper, ILogger<GetAllPostHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var text = "";
                if (request.SearchText != null) { text = request.SearchText; }
                var posts = await repository.GetAllAsync(x => x.Title.Contains(text)
                                                        || x.Content.Contains(text)
                                                        || x.Author.FirstName.Contains(text)
                                                        || x.Author.LastName.Contains(text)
                );
                if (posts == null)
                {
                    return ApiResponse.NotFound("Post Not Found.");
                }
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = posts.Count;
                posts = posts
                    .OrderByDescending(x => x.CreatedAt)
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
