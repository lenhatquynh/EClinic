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
    public class GetPostsOfHashTagHandler : IRequestHandler<GetPostsOfHashTagQuery, ObjectResult>
    {
        private readonly IMongoDBRepository<Post> postRepository;
        private readonly IMongoDBRepository<Answer> answerRepository;
        private readonly IMongoDBRepository<Hashtag> hashtagRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GetPostsOfHashTagHandler> logger;

        public GetPostsOfHashTagHandler(IMongoDBRepository<Post> postRepository, IMongoDBRepository<Answer> answerRepository, IMongoDBRepository<Hashtag> hashtagRepository, IMapper mapper, ILogger<GetPostsOfHashTagHandler> logger)
        {
            this.postRepository = postRepository;
            this.answerRepository = answerRepository;
            this.hashtagRepository = hashtagRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetPostsOfHashTagQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var searchText = request.SearchText;
                if (searchText == null) { searchText = ""; }
                var hashtags = await hashtagRepository.GetAllAsync(x => x.HashtagName.ToLower().Contains(searchText.ToLower()));
                if (hashtags == null)
                {
                    return ApiResponse.NotFound("Post Not Found.");
                }
                var ListHashtagID = hashtags.Select(x => x.Id).ToList();
                var answer = await answerRepository.GetAllAsync(x => x.Tags.Any(x => ListHashtagID.Contains(x)));
                if (answer == null)
                {
                    return ApiResponse.NotFound("Post Not Found.");
                }
                var listPostID = answer.Select(x => x.PostID).ToList();
                var posts = await postRepository.GetAllAsync(x => listPostID.Contains(x.Id));
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
