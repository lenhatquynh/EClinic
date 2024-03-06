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
    public class CreateReplyCommentHandler : IRequestHandler<CreateReplyCommentCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Comment> repository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateReplyCommentHandler> logger;
        private readonly ProfileService.ProfileServiceClient client;
        public CreateReplyCommentHandler(IConfiguration configuration, IMongoDBRepository<Comment> repository, IMapper mapper, ILogger<CreateReplyCommentHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(CreateReplyCommentCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await client.GetProfileAsync(new GetProfileRequest { UserID = request.UserID });
                if (string.IsNullOrEmpty(profile.UserID))
                {
                    throw new Exception("Get Profile Error");
                }
                Comment comment = await repository.GetAsync(request.CreateReplyCommentDtos.ParentCommentID);
                if (comment == null)
                {
                    return ApiResponse.NotFound("Parent Comment Not Found");
                }
                ReplyComment replyComment = mapper.Map<ReplyComment>(request.CreateReplyCommentDtos);
                replyComment.CreatedAt = DateTime.Now;
                replyComment.UpdatedAt = DateTime.Now;
                replyComment.Author = new Author();
                replyComment.Author.UserID = Guid.Parse(profile.UserID);
                replyComment.Author.FirstName = profile.FirstName;
                replyComment.Author.LastName = profile.LastName;
                replyComment.Author.Avatar = string.IsNullOrEmpty(profile.Avatar) ? ConstantsData.DefaultAvatarKey : profile.Avatar;
                comment.ReplyComments.Add(replyComment);
                await repository.UpdateAsync(comment);
                return ApiResponse.Created("Create Reply Comment Succes");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}

