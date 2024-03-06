using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Project.Common.Response;
using Project.CommunicateService.Commands;
using Project.CommunicateService.Data;
using Project.CommunicateService.Dtos.ChatMessageDtos;
using Project.CommunicateService.Hubs;
using Project.CommunicateService.Protos;
using Project.CommunicateService.Repository.ChatMessageRepositories;
using Project.CommunicateService.Repository.RoomRepositories;
using Project.Core.Logger;

namespace Project.CommunicateService.Handlers.ChatMessageHandlers
{
    public class CreateMessageHandler : IRequestHandler<CreateMessageCommand, ObjectResult>
    {
        private readonly IChatMessageRepository repository;
        private readonly ILogger<CreateMessageHandler> logger;
        private readonly IRoomRepository roomRepository;
        private readonly IHubContext<MessageHub> hubContext;
        private readonly IHubContext<MessageNotificationHub> hub;
        private readonly IMapper mapper;
        private readonly ProfileService.ProfileServiceClient client;
        public CreateMessageHandler(IConfiguration configuration, IChatMessageRepository repository, ILogger<CreateMessageHandler> logger, IRoomRepository roomRepository, IHubContext<MessageHub> hubContext, IHubContext<MessageNotificationHub> hub, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.roomRepository = roomRepository;
            this.hubContext = hubContext;
            this.hub = hub;
            this.mapper = mapper;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Room = await roomRepository.GetAsync(request.CreateMassageDtos.RoomID);
                if (Room.IsClosed)
                {
                    return ApiResponse.BadRequest("Room is close");
                }
                var UserID = Guid.Parse(request.UserID);
                if (Room.SenderID != UserID && Room.ReceiverID != Guid.Empty && Room.ReceiverID != UserID)
                {
                    return ApiResponse.BadRequest("This conversation has been answered by someone else");
                }
                var ChatMessage = new ChatMessage
                {
                    UserID = UserID,
                    Content = request.CreateMassageDtos.Content,
                    CreatedAt = DateTime.Now,
                    RoomID = request.CreateMassageDtos.RoomID,
                    Type = MessageType.Text
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
