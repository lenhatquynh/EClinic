using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Commands;
using Project.ForumService.Data;

namespace Project.ForumService.Handlers.CommentHandlers
{
    public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Comment> repository;
        private readonly ILogger<DeleteCommentHandler> logger;
        public DeleteCommentHandler(IMongoDBRepository<Comment> repository, ILogger<DeleteCommentHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(DeleteCommentCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await repository.GetAsync(request.CommentID);
                if (result == null)
                {
                    return ApiResponse.BadRequest("Comment Not Found");
                }
                await repository.RemoveAsync(request.CommentID);
                return ApiResponse.OK("Delete Comment Success.");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
