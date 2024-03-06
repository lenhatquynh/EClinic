using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Commands;
using Project.ForumService.Data;

namespace Project.ForumService.Handlers.PostHandlers
{
    public class AcceptPostHandler : IRequestHandler<AcceptPostCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Post> repository;
        private readonly ILogger<AcceptPostHandler> logger;

        public AcceptPostHandler(IMongoDBRepository<Post> repository, ILogger<AcceptPostHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(AcceptPostCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await repository.GetAsync(request.PostID);
                if (result == null)
                {
                    return ApiResponse.NotFound("Post not found.");
                }
                result.IsActive = !result.IsActive;
                await repository.UpdateAsync(result);
                if(result.IsActive)
                {
                    return ApiResponse.OK("Accept Post success. ");
                }
                return ApiResponse.OK("Reject Post success. ");

            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
