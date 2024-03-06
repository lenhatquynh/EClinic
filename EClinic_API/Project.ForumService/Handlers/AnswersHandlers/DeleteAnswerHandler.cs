using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Commands;
using Project.ForumService.Data;

namespace Project.ForumService.Handlers.AnswersHandlers
{
    public class DeleteAnswerHandler : IRequestHandler<DeleteAnswerCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Answer> repository;
        private readonly ILogger<DeleteAnswerHandler> logger;

        public DeleteAnswerHandler(IMongoDBRepository<Answer> repository, ILogger<DeleteAnswerHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(DeleteAnswerCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await repository.GetAsync(request.AnswerID);
                if (result == null)
                {
                    return ApiResponse.BadRequest("Answer Not Found");
                }
                await repository.RemoveAsync(request.AnswerID);
                return ApiResponse.OK("Delete Answer Success.");
            }
            catch(Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
