using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project.Common.Enum;
using Project.Common.Response;
using Project.CommunicateService.Commands;
using Project.CommunicateService.Data;
using Project.CommunicateService.Dtos.ChatMessageDtos;
using Project.CommunicateService.Hubs;
using Project.CommunicateService.Protos;
using Project.CommunicateService.Repository.ChatMessageRepositories;
using Project.CommunicateService.Repository.RoomRepositories;
using Project.Core.AWS;
using Project.Core.Logger;

namespace Project.CommunicateService.Handlers.ChatMessageHandlers
{
    public class CreateMessageFileHandler : IRequestHandler<CreateMessageFileCommand, ObjectResult>
    {
        private readonly IChatMessageRepository repository;
        private readonly IRoomRepository roomRepository;
        private readonly IAmazonS3Bucket s3Bucket;
        private readonly ILogger<CreateMessageFileHandler> logger;
        private readonly IHubContext<MessageHub> hubContext;
        private readonly IMapper mapper;
        private readonly ProfileService.ProfileServiceClient client;
        private readonly IHubContext<MessageNotificationHub> hub;
        public CreateMessageFileHandler(IConfiguration configuration, IChatMessageRepository repository, IRoomRepository roomRepository, IAmazonS3Bucket s3Bucket, ILogger<CreateMessageFileHandler> logger, IHubContext<MessageHub> hubContext, IMapper mapper, IHubContext<MessageNotificationHub> hub)
        {
            this.repository = repository;
            this.roomRepository = roomRepository;
            this.s3Bucket = s3Bucket;
            this.logger = logger;
            this.hubContext = hubContext;
            this.mapper = mapper;
            this.hub = hub;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(CreateMessageFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Room = await roomRepository.GetAsync(request.CreateMassageFileDtos.RoomID);
                if (Room.IsClosed)
                {
                    return ApiResponse.BadRequest("Room is close");
                }
                var UserID = Guid.Parse(request.UserID);
                if (Room.SenderID != UserID && Room.ReceiverID != Guid.Empty && Room.ReceiverID != UserID)
                {
                    return ApiResponse.BadRequest("This conversation has been answered by someone else");
                }
                var url = await s3Bucket.UploadFileAsync(request.CreateMassageFileDtos.File, FileType.Image);
                var ChatMessage = new ChatMessage
                {
                    UserID = UserID,
                    Content = url,
                    CreatedAt = DateTime.Now,
                    RoomID = request.CreateMassageFileDtos.RoomID,
                    Type = MessageType.Image
                };
                var result = await repository.CreateAsync(ChatMessage);
                if (!result)
                {
                    throw new Exception("Create chat message error.");
                }
                var chatDtos = mapper.Map<ChatMessageDto>(ChatMessage);
                if (Room.ReceiverID == Guid.Empty && Room.SenderID != UserID)
                {
                    Room.ReceiverID = UserID;
                    await roomRepository.UpdateAsync(Room);
                    var ListUserID = new List<Guid>();
                    ListUserID.Add(Room.ReceiverID);
                    ListUserID.Add(Room.SenderID);
                    GetAllProfileRequest getAllProfileRequest = new GetAllProfileRequest();
                    getAllProfileRequest.UserIDs.AddRange(ListUserID.ConvertAll(g => g.ToString()));
                    var response = await client.GetAllProfileAsync(getAllProfileRequest);
                    if (response is null)
                    {
                        return ApiResponse.NotFound("Get Profile Error");
                    }
                    var profiles = response.Profiles;
                    await hubContext.Clients.Group(ChatMessage.RoomID.ToString()).SendAsync("NewAnswer", profiles[0]);
                }
                else if (Room.ReceiverID == Guid.Empty && Room.SenderID == UserID)
                {
                    var ListUserID = new List<Guid>();
                    ListUserID.Add(Room.ReceiverID);
                    ListUserID.Add(Room.SenderID);
                    GetAllProfileRequest getAllProfileRequest = new GetAllProfileRequest();
                    getAllProfileRequest.UserIDs.AddRange(ListUserID.ConvertAll(g => g.ToString()));
                    var response = await client.GetAllProfileAsync(getAllProfileRequest);
                    if (response is null)
                    {
                        return ApiResponse.NotFound("Get Profile Error");
                    }
                    var profiles = response.Profiles;
                    await hub.Clients.All.SendAsync("Response", profiles[1], chatDtos, Room.RoomID);
                }
                await hubContext.Clients.Group(ChatMessage.RoomID.ToString()).SendAsync("Response", chatDtos);
                return ApiResponse.Created("Create Success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
