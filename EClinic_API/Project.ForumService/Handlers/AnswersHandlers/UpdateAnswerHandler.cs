using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Commands;
using Project.ForumService.Data;

namespace Project.ForumService.Handlers.AnswersHandlers
{
    public class UpdateAnswerHandler : IRequestHandler<UpdateAnswerCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Answer> repository;
        private readonly ILogger<UpdateAnswerHandler> logger;

        public UpdateAnswerHandler(IMongoDBRepository<Answer> repository, ILogger<UpdateAnswerHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(UpdateAnswerCommands request, CancellationToken cancellationToken)
        {
            try
            {
                Answer answer = await repository.GetAsync(request.UpdateAnswerDtos.AnswerID);
                if (answer == null)
                {
                    return ApiResponse.NotFound("Answer not found");
                }
                answer.Content = request.UpdateAnswerDtos.Content;
                answer.Tags = request.UpdateAnswerDtos.Tags;
                answer.UpdatedAt = DateTime.Now;
                await repository.UpdateAsync(answer);
                return ApiResponse.OK("Update Answer Success.");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
