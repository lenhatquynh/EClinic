using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Data;
using Project.ForumService.Dtos.AnswersDtos;
using Project.ForumService.Dtos.HashtagDtos;
using Project.ForumService.Queries;

namespace Project.ForumService.Handlers.AnswersHandlers
{
    public class GetAnswerHandler : IRequestHandler<GetAnswerQuery, ObjectResult>
    {
        private readonly IMongoDBRepository<Answer> repository;
        private readonly IMongoDBRepository<Hashtag> hashTagRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GetAnswerHandler> logger;

        public GetAnswerHandler(IMongoDBRepository<Answer> repository, IMongoDBRepository<Hashtag> hashTagRepository, IMapper mapper, ILogger<GetAnswerHandler> logger)
        {
            this.repository = repository;
            this.hashTagRepository = hashTagRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetAnswerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Answer answer = await repository.GetAsync(a => a.PostID == request.PostID);
                if (answer == null)
                {
                    return ApiResponse.NotFound("Answer Not Found.");
                }
                AnswerDtos answerDtos = mapper.Map<AnswerDtos>(answer);
                var Tags = await hashTagRepository.GetAllAsync();
                var TagIDs = answer.Tags;
                var HashTags = Tags.Where(t => TagIDs.Contains(t.Id)).ToList();
                answerDtos.HashTags = mapper.Map<List<HashtagsDtos>>(HashTags);
                answerDtos.IsLike = false;
                if (!string.IsNullOrEmpty(request.UserID))
                {
                    var userID = Guid.Parse(request.UserID);
                    if (answer.LikeUserIds.Count > 0)
                    {
                        answerDtos.IsLike = answer.LikeUserIds.Contains(userID);
                    }
                }
                return ApiResponse.OK(answerDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
