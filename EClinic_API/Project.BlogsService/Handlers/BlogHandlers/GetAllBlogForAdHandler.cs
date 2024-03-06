using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using Project.BlogService.Data;
using Project.BlogService.Dtos.BlogDtos;
using Project.BlogService.Queries;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;

namespace Project.BlogService.Handlers.BlogHandlers
{
    public class GetAllBlogForAdHandler : IRequestHandler<GetAllBlogForAdQuery, ObjectResult>
    {
        private readonly IMongoDBRepository<Blog> repository;
        private readonly IMongoDBRepository<Hashtag> hashTagRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GetAllBlogForAdHandler> logger;
        public GetAllBlogForAdHandler(IMongoDBRepository<Blog> repository, IMongoDBRepository<Hashtag> hashTagRepository, IMapper mapper, ILogger<GetAllBlogForAdHandler> logger)
        {
            this.repository = repository;
            this.hashTagRepository = hashTagRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetAllBlogForAdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var blogs = await repository.GetAllAsync();
                if (blogs == null)
                {
                    return ApiResponse.NotFound("Blogs Not Found.");
                }
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = blogs.Count;
                blogs = blogs
                    .OrderBy(x => x.CreatedAt)
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();
               
                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;

                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
                List<BlogDtos> blogDtos = mapper.Map<List<BlogDtos>>(blogs);
                // convert from hashTagIDs of blog into hashTag list of blogDtos
                var hashTags = await hashTagRepository.GetAllAsync();
                foreach (int index in Enumerable.Range(0, blogDtos.Count))
                {
                    BlogDtos blogDto = blogDtos[index];
                    var hashTagIds = blogs[index].HashtagId;
                    blogDto.Hashtags = hashTags.Where(t => hashTagIds.Contains(t.Id)).ToList();
                }

                return ApiResponse.OK<List<BlogDtos>>(blogDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
