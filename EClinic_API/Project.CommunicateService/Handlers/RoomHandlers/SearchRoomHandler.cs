using AutoMapper;
using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.CommunicateService.Data;
using Project.CommunicateService.Dtos.RoomDtos;
using Project.CommunicateService.Protos;
using Project.CommunicateService.Queries;
using Project.CommunicateService.Repository.RoomRepositories;
using Project.Core.Logger;

namespace Project.CommunicateService.Handlers.RoomHandlers
{
    public class SearchRoomHandler : IRequestHandler<SearchRoomQuery, ObjectResult>
    {
        private readonly IRoomRepository roomRepository;
        private readonly IMapper mapper;
        private readonly ProfileService.ProfileServiceClient client;
        private readonly ILogger<SearchRoomHandler> logger;
        public SearchRoomHandler(IConfiguration configuration, IRoomRepository roomRepository, IMapper mapper, ILogger<SearchRoomHandler> logger)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
            this.logger = logger;
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ProfileServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
            client = new ProfileService.ProfileServiceClient(channel);
        }
        public async Task<ObjectResult> Handle(SearchRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var UserID = Guid.Parse(request.UserID);
                var Rooms = await roomRepository.GetAllRoom(room => room.ReceiverID == UserID || room.SenderID == UserID);
                if (Rooms.Count == 0)
                {
                    return ApiResponse.NotFound("Room Not Found");
                }
                var ListOrtherUserID = new List<Guid>();
                foreach (Room room in Rooms)
                {
                    if (room.ChatMessages.Count == 0)
                    {
                        room.ChatMessages.Add(new ChatMessage { CreatedAt = room.CreatedAt, Content = "" });
                    }
                    room.ChatMessages = room.ChatMessages.OrderByDescending(x => x.CreatedAt).ToList();
                }
                foreach (var room in Rooms)
                {
                    ListOrtherUserID.Add(room.SenderID != UserID ? room.SenderID : room.ReceiverID);
                }
                GetAllProfileRequest getAllProfileRequest = new GetAllProfileRequest();
                getAllProfileRequest.UserIDs.AddRange(ListOrtherUserID.ConvertAll(g => g.ToString()));
                var response = await client.GetAllProfileAsync(getAllProfileRequest);
                if (response is null)
                {
                    return ApiResponse.NotFound("Room Not Found");
                }
                var profiles = response.Profiles.ToList();
                Rooms = Rooms.OrderByDescending(x => x.ChatMessages.OrderByDescending(x => x.CreatedAt).ToList()[0].CreatedAt).ToList();
                var RoomDtos = mapper.Map<List<RoomDto>>(Rooms);
                for (int i = 0; i <= RoomDtos.Count - 1; i++)
                {
                    RoomDtos[i].RoomAuthor = new Author();
                    RoomDtos[i].RoomAuthor.UserID = Guid.Parse(profiles[i].UserID);
                    RoomDtos[i].RoomAuthor.FirstName = profiles[i].FirstName;
                    RoomDtos[i].RoomAuthor.LastName = profiles[i].LastName;
                    RoomDtos[i].RoomAuthor.Avatar = profiles[i].Avatar;
                }
                RoomDtos = RoomDtos.Where(x => x.RoomAuthor.FirstName.Contains(request.SearchText) || x.RoomAuthor.LastName.Contains(request.SearchText)).ToList();
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = Rooms.Count;
                RoomDtos = RoomDtos
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();

                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;
                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
                return ApiResponse.OK(RoomDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
