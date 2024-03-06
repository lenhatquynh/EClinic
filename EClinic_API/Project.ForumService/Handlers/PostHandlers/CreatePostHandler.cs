using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Enum;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Commands;
using Project.ForumService.Data;
using Project.ForumService.Dtos.Model;
using Project.ForumService.Protos;

namespace Project.ForumService.Handlers.PostHandlers
{
    public class CreatePostHandler : IRequestHandler<CreatePostCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Post> repository;
        private readonly IMapper mapper;
        private readonly ILogger<CreatePostHandler> logger;
        private readonly IAmazonS3Bucket bucket;
        private readonly ProfileService.ProfileServiceClient client;
        public CreatePostHandler(IConfiguration configuration, IMongoDBRepository<Post> repository, IMapper mapper, ILogger<CreatePostHandler> logger, IAmazonS3Bucket bucket)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.bucket = bucket;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(CreatePostCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await client.GetProfileAsync(new GetProfileRequest { UserID = request.UserID });
                if (string.IsNullOrEmpty(profile.UserID))
                {
                    throw new Exception("Get Profile Error");
                }
                Post post = mapper.Map<Post>(request.createPostDtos);
                post.CreatedAt = DateTime.Now;
                post.UpdatedAt = DateTime.Now;
                post.IsActive = false;
                post.Author = new Author();
                post.Author.UserID = Guid.Parse(profile.UserID);
                post.Author.FirstName = profile.FirstName;
                post.Author.LastName = profile.LastName;
                post.Author.Avatar = string.IsNullOrEmpty(profile.Avatar) ? ConstantsData.DefaultAvatarKey : profile.Avatar;
                if (request.createPostDtos.Images != null)
                {
                    post.Image = await bucket.UploadManyFileAsync(request.createPostDtos.Images, FileType.Image);
                }

                await repository.CreateAsync(post);
                return ApiResponse.Created("Create Post Success");

            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}

