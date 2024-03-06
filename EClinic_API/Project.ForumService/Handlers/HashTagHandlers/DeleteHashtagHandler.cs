using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Commands;
using Project.ForumService.Data;

namespace Project.ForumService.Handlers.HashTagHandlers
{
    public class DeleteHashtagHandler : IRequestHandler<DeleteHashtagCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Hashtag> repository;
        private readonly IMongoDBRepository<Answer> answerRepository;
        private readonly ILogger<GetAllHashTagHandler> logger;

        public DeleteHashtagHandler(IMongoDBRepository<Hashtag> repository, ILogger<GetAllHashTagHandler> logger, IMongoDBRepository<Answer> answerRepository)
        {
            this.repository = repository;
            this.logger = logger;
            this.answerRepository = answerRepository;
        }

        public async Task<ObjectResult> Handle(DeleteHashtagCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var hashtags = await repository.GetAsync(request.HashtagID);
                if (hashtags == null)
                {
                    return ApiResponse.NotFound("Hashtag not found.");
                }
                var answers = await answerRepository.GetAllAsync(x => x.Tags.Contains(request.HashtagID));
                if(answers.Count > 0)
                {
                    foreach (Answer answer in answers)
                    {
                        answer.Tags.Remove(request.HashtagID);
                    }
                    await answerRepository.UpdateManyAsync(answers);
                }
                await repository.RemoveAsync(request.HashtagID);
                return ApiResponse.OK("Delete success.");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
