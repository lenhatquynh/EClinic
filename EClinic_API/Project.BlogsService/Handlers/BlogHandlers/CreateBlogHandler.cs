using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project.BlogService.Commands;
using Project.BlogService.Data;
using Project.BlogService.Dtos.BlogDtos;
using Project.BlogService.Protos;
using Project.Common.Enum;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;

namespace Project.BlogService.Handlers.BlogHandlers
{
    public class CreateBlogHandler : IRequestHandler<CreateBlogCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Blog> repository;
        private readonly IMongoDBRepository<Hashtag> hashtagRepository;
        private readonly ILogger<CreateBlogHandler> logger;
        private readonly IAmazonS3Bucket bucket;
        private readonly IMapper mapper;
        private readonly ProfileService.ProfileServiceClient client;

        public CreateBlogHandler(IConfiguration configuration, IMongoDBRepository<Blog> repository, IMongoDBRepository<Hashtag> hashtagRepository, ILogger<CreateBlogHandler> logger, IAmazonS3Bucket bucket, IMapper mapper)
        {
            this.repository = repository;
            this.hashtagRepository = hashtagRepository;
            this.logger = logger;
            this.bucket = bucket;
            this.mapper = mapper;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(CreateBlogCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await client.GetProfileAsync(new GetProfileRequest { UserID = request.UserId });
                if (string.IsNullOrEmpty(profile.UserID))
                {
                    throw new Exception("Get Profile Error");
                }
                Blog blog = mapper.Map<Blog>(request.createBlogDtos);

                if (request.createBlogDtos.CoverImage is not null) { 
                    var coverImageUrl = await bucket.UploadFileAsync(request.createBlogDtos.CoverImage, FileType.Image);
                    blog.CoverImage = coverImageUrl;
                }
                // get Author through profile
                blog.Author = new Author();
                blog.Author.UserID = Guid.Parse(profile.UserID);
                blog.Author.FirstName = profile.FirstName;
                blog.Author.LastName = profile.LastName;
                blog.Author.Avatar = profile.Avatar;

                blog.CreatedAt = DateTime.Now;
                blog.UpdatedAt = DateTime.Now;
                await repository.CreateAsync(blog);

                // update count for hashtag
                var hashTagIds = blog.HashtagId;
                var hashTags = await hashtagRepository.GetAllAsync();
                var haveHashtags = hashTags.Where(t => hashTagIds.Contains(t.Id)).ToList();
                haveHashtags.ForEach(hashTag =>
                {
                    hashTag.Count++;
                });

                if (haveHashtags.Count > 0)
                {
                    await hashtagRepository.UpdateManyAsync(haveHashtags);
                }

                return ApiResponse.OK("Create Success.");

            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
