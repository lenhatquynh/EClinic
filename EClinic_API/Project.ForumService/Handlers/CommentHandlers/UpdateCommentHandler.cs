using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Commands;
using Project.ForumService.Data;

namespace Project.ForumService.Handlers.CommentHandlers
{
    public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Comment> repository;
        private readonly ILogger<UpdateCommentHandler> logger;
        public UpdateCommentHandler(IMongoDBRepository<Comment> repository, ILogger<UpdateCommentHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(UpdateCommentCommands request, CancellationToken cancellationToken)
        {
            try
            {
                Comment comment = await repository.GetAsync(request.UpdateCommentDtos.CommentID);
                if (comment == null)
                {
                    return ApiResponse.NotFound("Comment not found");
                }
                comment.Content = request.UpdateCommentDtos.Content;
                comment.UpdatedAt = DateTime.Now;
                await repository.UpdateAsync(comment);
                return ApiResponse.OK("Update Comment Success.");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
