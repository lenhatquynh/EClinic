using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.CommunicateService.Data;
using Project.CommunicateService.Dtos.ChatMessageDtos;
using Project.CommunicateService.Dtos.RoomDtos;
using Project.CommunicateService.Protos;
using Project.CommunicateService.Queries;
using Project.CommunicateService.Repository.RoomRepositories;
using Project.Core.Logger;

namespace Project.CommunicateService.Handlers.RoomHandlers
{
    public class GetAllMessageOfRoomHandler : IRequestHandler<GetAllMessageOfRoomQuery, ObjectResult>
    {
        private readonly IRoomRepository roomRepository;
        private readonly IMapper mapper;
        private readonly ProfileService.ProfileServiceClient client;
        private readonly ILogger<GetAllMessageOfRoomHandler> logger;

        public GetAllMessageOfRoomHandler(IConfiguration configuration, IRoomRepository roomRepository, IMapper mapper, ILogger<GetAllMessageOfRoomHandler> logger)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
            this.logger = logger;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }

        public async Task<ObjectResult> Handle(GetAllMessageOfRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var UserID = Guid.Parse(request.UserID);
                var Room = await roomRepository.GetRoom(request.RoomID);
                if (Room == null)
                {
                    return ApiResponse.NotFound("Room Not Found");
                }
                var ChatMessages = Room.ChatMessages;
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = ChatMessages.Count;
                ChatMessages = ChatMessages
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();
                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;
                var ChatMessageDtos = mapper.Map<List<ChatMessageDto>>(ChatMessages);
                foreach (var chatMessage in ChatMessageDtos)
                {
                    if (chatMessage.UserID == UserID)
                    {
                        chatMessage.IsMyChat = true;
                    }
                }
                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
                ChatMessageDtos = ChatMessageDtos.OrderBy(x => x.CreatedAt).ToList();
                var ListUserID = new List<Guid>();
                ListUserID.Add(UserID);
                ListUserID.Add(Room.SenderID != UserID ? Room.SenderID : Room.ReceiverID);
                GetAllProfileRequest getAllProfileRequest = new GetAllProfileRequest();
                getAllProfileRequest.UserIDs.AddRange(ListUserID.ConvertAll(g => g.ToString()));
                var response = await client.GetAllProfileAsync(getAllProfileRequest);
                if (response is null)
                {
                    return ApiResponse.NotFound("Get Profile Error");
                }
                var profiles = response.Profiles;
                ChatResponseDtos chatResponseDtos = new ChatResponseDtos();
                chatResponseDtos.Message = ChatMessageDtos;
                chatResponseDtos.MyProfile = new Author();
                chatResponseDtos.MyProfile.UserID = Guid.Parse(profiles[0].UserID);
                chatResponseDtos.MyProfile.FirstName = profiles[0].FirstName;
                chatResponseDtos.MyProfile.LastName = profiles[0].LastName;
                chatResponseDtos.MyProfile.Avatar = profiles[0].Avatar;
                chatResponseDtos.OtherProfile = new Author();
                chatResponseDtos.OtherProfile.UserID = Guid.Parse(profiles[1].UserID);
                chatResponseDtos.OtherProfile.FirstName = profiles[1].FirstName;
                chatResponseDtos.OtherProfile.LastName = profiles[1].LastName;
                chatResponseDtos.OtherProfile.Avatar = profiles[1].Avatar;
                return ApiResponse.OK<ChatResponseDtos>(chatResponseDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
