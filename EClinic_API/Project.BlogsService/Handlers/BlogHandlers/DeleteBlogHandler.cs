using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BlogService.Commands;
using Project.BlogService.Data;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;

namespace Project.ForumService.Handlers.PostHandlers
{
    public class DeleteBlogHandler : IRequestHandler<DeleteBlogCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Blog> repository;
        private readonly IMongoDBRepository<Hashtag> hashtagRepository;
        private readonly ILogger<DeleteBlogHandler> logger;
        //private readonly IAmazonS3Bucket bucket;

        public DeleteBlogHandler(IMongoDBRepository<Blog> repository, IMongoDBRepository<Hashtag> hashtagRepository, ILogger<DeleteBlogHandler> logger, IAmazonS3Bucket bucket)
        {
            this.repository = repository;
            this.hashtagRepository = hashtagRepository;
            this.logger = logger;
            //this.bucket = bucket;
        }

        public async Task<ObjectResult> Handle(DeleteBlogCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var blog = await repository.GetAsync(request.BlogID);
                if (blog == null)
                {
                    return ApiResponse.BadRequest("Blog Not Found");
                }
                await repository.RemoveAsync(request.BlogID);

                // update count for hashtag
                var hashTagIds = blog.HashtagId;
                var hashTags = await hashtagRepository.GetAllAsync();
                var haveHashtags = hashTags.Where(t => hashTagIds.Contains(t.Id)).ToList();
                haveHashtags.ForEach(hashTag =>
                {
                    if (hashTag.Count > 0)
                    {
                        hashTag.Count--;
                    }
                });

                await hashtagRepository.UpdateManyAsync(haveHashtags);

                return ApiResponse.OK("Delete Blog Success.");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
