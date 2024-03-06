using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.BlogService.Data;
using Project.BlogService.Dtos.BlogDtos;
using Project.BlogService.Queries;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;

namespace Project.BlogService.Handlers.PostHandlers
{
    public class GetBlogsForAdHandler : IRequestHandler<GetBlogsForAdQuery, ObjectResult>
    {
        private readonly IMongoDBRepository<Blog> blogRepository;
        private readonly IMongoDBRepository<Hashtag> hashtagRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GetBlogsForAdHandler> logger;

        public GetBlogsForAdHandler(IMongoDBRepository<Blog> blogRepository, IMongoDBRepository<Hashtag> hashtagRepository, IMapper mapper, ILogger<GetBlogsForAdHandler> logger)
        {
            this.blogRepository = blogRepository;
            this.hashtagRepository = hashtagRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetBlogsForAdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var searchText = request.SearchBlogDtos.SearchText;
                if (searchText == null) { searchText = ""; }
                var blogs = await blogRepository.GetAllAsync(x => x.Title.ToLower().Contains(searchText.ToLower()) || x.Content.ToLower().Contains(searchText.ToLower()));
                if (blogs == null)
                {
                    return ApiResponse.NotFound("Blog Not Found.");
                }

                // search based on hashtag
                if (request.SearchBlogDtos.Tags?.Count > 0)
                {
                    blogs = blogs.Where(blog => request.SearchBlogDtos.Tags.All(id => blog.HashtagId.Contains(id))).ToList();
                }

                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = blogs.Count;
                blogs = blogs
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();

                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;

                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
                List<BlogDtos> blogDtos = mapper.Map<List<BlogDtos>>(blogs);
                // convert from hashTagIDs of list blogs into hashTag list of list blogDtos
                var hashTags = await hashtagRepository.GetAllAsync();
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
