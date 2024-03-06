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
using Project.ForumService.Dtos.PostsDtos;
using Project.ForumService.Protos;

namespace Project.ForumService.Handlers.PostHandlers
{
    public class CreateAnswerHandler : IRequestHandler<CreateAnswerCommands, ObjectResult>
    {
        private readonly IMongoDBRepository<Answer> repository;
        private readonly IMongoDBRepository<Hashtag> hashtagrepository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateAnswerHandler> logger;
        private readonly ProfileService.ProfileServiceClient client;

        public CreateAnswerHandler(IConfiguration configuration, IMongoDBRepository<Answer> repository, IMapper mapper, ILogger<CreateAnswerHandler> logger, IMongoDBRepository<Hashtag> hashtagrepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.hashtagrepository = hashtagrepository;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(CreateAnswerCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await client.GetProfileAsync(new GetProfileRequest { UserID = request.UserID });
                if (string.IsNullOrEmpty(profile.UserID))
                {
                    throw new Exception("Get Profile Error");
                }
                Answer answer = mapper.Map<Answer>(request.CreateAnswerDtos);
                answer.CreatedAt = DateTime.Now;
                answer.UpdatedAt = DateTime.Now;
                var hashtags = new List<Hashtag>();
                answer.Author = new Author();
                answer.Author.UserID = Guid.Parse(profile.UserID);
                answer.Author.FirstName = profile.FirstName;
                answer.Author.LastName = profile.LastName;
                answer.Author.Avatar = string.IsNullOrEmpty(profile.Avatar) ? ConstantsData.DefaultAvatarKey : profile.Avatar;
                foreach (Guid tag in answer.Tags)
                {
                    hashtags.Add(await hashtagrepository.GetAsync(tag));
                }
                foreach (Hashtag tag in hashtags)
                {
                    tag.Count = ++tag.Count;
                }
                await hashtagrepository.UpdateManyAsync(hashtags);
                await repository.CreateAsync(answer);
                return ApiResponse.Created("Create Answer Success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}

