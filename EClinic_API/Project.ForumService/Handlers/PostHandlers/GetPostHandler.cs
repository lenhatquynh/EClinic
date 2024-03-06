using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Data;
using Project.ForumService.Dtos.PostsDtos;
using Project.ForumService.Queries;

namespace Project.ForumService.Handlers.PostHandlers
{
    public class GetPostHandler : IRequestHandler<GetPostQuery, ObjectResult>
    {
        private readonly IMongoDBRepository<Post> repository;
        private readonly IMapper mapper;
        private readonly ILogger<GetPostHandler> logger;

        public GetPostHandler(IMongoDBRepository<Post> repository, IMapper mapper, ILogger<GetPostHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Post post = await repository.GetAsync(request.PostID);
                if (post == null)
                {
                    return ApiResponse.NotFound("Post Not Found.");
                }
                if (!post.IsActive)
                {
                    return ApiResponse.NotFound("Post Not Found.");
                }
                PostDtos postDtos = mapper.Map<PostDtos>(post);
                if(!string.IsNullOrEmpty(request.UserID))
                {
                    var userID = Guid.Parse(request.UserID);
                    if (post.LikeUserIds.Count > 0)
                    {
                        postDtos.IsLike = post.LikeUserIds.Contains(userID);
                    }
                }
                return ApiResponse.OK(postDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
