using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Commands;
using Project.ForumService.Data;
using Project.ForumService.Dtos.Model;
using Project.ForumService.Protos;

namespace Project.ForumService.Handlers.CommentHandlers
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Comment> repository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateCommentHandler> logger;
        private readonly ProfileService.ProfileServiceClient client;
        public CreateCommentHandler(IConfiguration configuration, IMongoDBRepository<Comment> repository, IMapper mapper, ILogger<CreateCommentHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(CreateCommentCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await client.GetProfileAsync(new GetProfileRequest { UserID = request.UserID });
                if (string.IsNullOrEmpty(profile.UserID))
                {
                    throw new Exception("Get Profile Error");
                }
                Comment comment = mapper.Map<Comment>(request.CreateCommentDtos);
                comment.CreatedAt = DateTime.Now;
                comment.UpdatedAt = DateTime.Now;
                comment.Author = new Author();
                comment.Author.UserID = Guid.Parse(profile.UserID);
                comment.Author.FirstName = profile.FirstName;
                comment.Author.LastName = profile.LastName;
                comment.Author.Avatar = string.IsNullOrEmpty(profile.Avatar) ? ConstantsData.DefaultAvatarKey : profile.Avatar;
                await repository.CreateAsync(comment);
                return ApiResponse.Created("Create Comment Succes");

            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}

