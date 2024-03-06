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
    public class GetPostsSortByLikeHandler : IRequestHandler<GetPostsSortByLikeQuery, ObjectResult>
    {
        private readonly IMongoDBRepository<Post> postRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GetPostsSortByLikeHandler> logger;

        public GetPostsSortByLikeHandler(IMongoDBRepository<Post> postRepository, IMapper mapper, ILogger<GetPostsSortByLikeHandler> logger)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetPostsSortByLikeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var posts = await postRepository.GetAllAsync();
                if (posts == null)
                {
                    return ApiResponse.NotFound("Post Not Found.");
                }
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = posts.Count;
                posts = posts
                    .OrderByDescending(x => x.LikeUserIds.Count)
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
