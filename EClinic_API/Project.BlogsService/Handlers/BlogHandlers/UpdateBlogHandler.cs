using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BlogService.Commands;
using Project.BlogService.Data;
using Project.Common.Enum;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;

namespace Project.ForumService.Handlers.BlogHandlers;

public class UpdateBlogHandler : IRequestHandler<UpdateBlogCommands, ObjectResult>
{
    private readonly IMongoDBRepository<Blog> repository;
    private readonly IMongoDBRepository<Hashtag> hashtagRepository;
    private readonly ILogger<UpdateBlogHandler> logger;
    private readonly IAmazonS3Bucket bucket;

    public UpdateBlogHandler(IMongoDBRepository<Blog> repository, IMongoDBRepository<Hashtag> hashtagRepository, ILogger<UpdateBlogHandler> logger, IAmazonS3Bucket bucket)
    {
        this.repository = repository;
        this.hashtagRepository = hashtagRepository;
        this.logger = logger;
        this.bucket = bucket;
    }

    public async Task<ObjectResult> Handle(UpdateBlogCommands request, CancellationToken cancellationToken)
    {
        try
        {
            Blog blog = await repository.GetAsync(request.updateBlogDtos.Id);
            if (blog == null)
            {
                return ApiResponse.NotFound("Blog not found");
            }
            if (request.updateBlogDtos.CoverImage is not null)
            {
                var coverImageUrl = await bucket.UploadFileAsync(request.updateBlogDtos.CoverImage, FileType.Image);
                blog.CoverImage = coverImageUrl;
            }

            // update count for hashtag
            var subtractHashTagIds = blog.HashtagId;
            var addHashTagIds = request.updateBlogDtos.HashtagId;

            blog.Title = request.updateBlogDtos.Title;
            blog.Content = request.updateBlogDtos.Content;
            blog.IsActive = request.updateBlogDtos.IsActive;
            blog.MetaTitle = request.updateBlogDtos.MetaTitle;
            blog.MetaDescription = request.updateBlogDtos.MetaDescription;
            blog.MetaKeywords = request.updateBlogDtos.MetaKeywords;
            blog.HashtagId = request.updateBlogDtos.HashtagId;
            blog.UpdatedAt = DateTime.Now;
           
            await repository.UpdateAsync(blog);

            // update count for hashtag
            var hashTags = await hashtagRepository.GetAllAsync();
            var subtractHashtags = hashTags.Where(t => subtractHashTagIds.Contains(t.Id)).ToList();
            var addHashtags = hashTags.Where(t => addHashTagIds.Contains(t.Id)).ToList();
            subtractHashtags.ForEach(hashTag =>
            {
                if (hashTag.Count > 0)
                {
                    hashTag.Count--;
                }
            });
            addHashtags.ForEach(hashTag =>
            {
                hashTag.Count++;
            });
            if (subtractHashtags.Count > 0)
            {
                await hashtagRepository.UpdateManyAsync(subtractHashtags);
            }
            if (addHashtags.Count > 0)
            {
                await hashtagRepository.UpdateManyAsync(addHashtags);
            }

            return ApiResponse.OK("Update Blog Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }
    }
}
