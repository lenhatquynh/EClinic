using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BlogService.Data;
using Project.BlogService.Dtos.BlogDtos;
using Project.BlogService.Queries;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;

namespace Project.BlogService.Handlers.BlogHandlers
{
    public class GetBlogHandler : IRequestHandler<GetBlogQuery, ObjectResult>
    {
        private readonly IMongoDBRepository<Blog> repository;
        private readonly IMongoDBRepository<Hashtag> hashTagRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GetBlogHandler> logger;

        public GetBlogHandler(IMongoDBRepository<Blog> repository, IMongoDBRepository<Hashtag> hashTagRepository, IMapper mapper, ILogger<GetBlogHandler> logger)
        {
            this.repository = repository;
            this.hashTagRepository = hashTagRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetBlogQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Blog blog = await repository.GetAsync(request.BlogID);
                if (blog == null)
                {
                    return ApiResponse.NotFound("Blog Not Found.");
                }
                if (!blog.IsActive)
                {
                    return ApiResponse.NotFound("Blog Not Found.");
                }
                BlogDtos blogDtos = mapper.Map<BlogDtos>(blog);
                // convert from hashTagIDs of blog into hashTag list of blogDtos
                var hashTagIds = blog.HashtagId;
                var hashTags = await hashTagRepository.GetAllAsync();
                blogDtos.Hashtags = hashTags.Where(t => hashTagIds.Contains(t.Id)).ToList();

                return ApiResponse.OK(blogDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
