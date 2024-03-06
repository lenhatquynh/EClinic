using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Enum;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Commands;
using Project.ForumService.Data;

namespace Project.ForumService.Handlers.PostHandlers
{
    public class UpdateCommentHandler : IRequestHandler<UpdatePostCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Post> repository;
        private readonly ILogger<UpdateCommentHandler> logger;
        private readonly IAmazonS3Bucket bucket;

        public UpdateCommentHandler(IMongoDBRepository<Post> repository, ILogger<UpdateCommentHandler> logger, IAmazonS3Bucket bucket)
        {
            this.repository = repository;
            this.logger = logger;
            this.bucket = bucket;
        }

        public async Task<ObjectResult> Handle(UpdatePostCommands request, CancellationToken cancellationToken)
        {
            try
            {
                Post post = await repository.GetAsync(request.updatePostDtos.PostID);
                if (post == null)
                {
                    return ApiResponse.NotFound("Post not found");
                }
                post.Title = request.updatePostDtos.Title;
                post.Content = request.updatePostDtos.Content;
                post.UpdatedAt = DateTime.Now;
                if(request.updatePostDtos.Images != null)
                {
                    var x = await bucket.UploadManyFileAsync(request.updatePostDtos.Images, FileType.Image);
                    post.Image.AddRange(x);
                }
               
                await repository.UpdateAsync(post);
                return ApiResponse.OK("Update Post Success.");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
