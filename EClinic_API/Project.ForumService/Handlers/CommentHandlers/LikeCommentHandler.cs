using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Commands;
using Project.ForumService.Data;
using Project.ForumService.Queries;

namespace Project.ForumService.Handlers.CommentHandlers
{
    public class LikeCommentHandler : IRequestHandler<LikeCommentCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Comment> repository;
        private readonly ILogger<LikeCommentHandler> logger;

        public LikeCommentHandler(IMongoDBRepository<Comment> repository, ILogger<LikeCommentHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(LikeCommentCommands request, CancellationToken cancellationToken)
        {
            try
            {
                Comment comment = await repository.GetAsync(request.CommentID);
                if (comment == null)
                {
                    return ApiResponse.NotFound("Post Not Found.");
                }
                var userID = Guid.Parse(request.UserID);
                var result = comment.LikeUserIds.Contains(userID);
                if (result)
                {
                    comment.LikeUserIds.Remove(userID);
                }
                else
                {
                    comment.LikeUserIds.Add(userID);
                }
                await repository.UpdateAsync(comment);
                return ApiResponse.OK("Update Like Comment Success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
