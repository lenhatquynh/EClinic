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
    public class GetPostNoAnswerHandler : IRequestHandler<GetPostNoAnswerQuery, ObjectResult>
    {
        private readonly IMongoDBRepository<Post> repository;
        private readonly IMongoDBRepository<Answer> answerRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GetPostNoAnswerHandler> logger;

        public GetPostNoAnswerHandler(IMongoDBRepository<Post> repository, IMongoDBRepository<Answer> answerRepository, IMapper mapper, ILogger<GetPostNoAnswerHandler> logger)
        {
            this.repository = repository;
            this.answerRepository = answerRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetPostNoAnswerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var answer = await answerRepository.GetAllAsync();
                var ListAnswer = answer.Select(x=>x.PostID).ToList();
                var posts = await repository.GetAllAsync(x => !ListAnswer.Contains(x.Id));
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
