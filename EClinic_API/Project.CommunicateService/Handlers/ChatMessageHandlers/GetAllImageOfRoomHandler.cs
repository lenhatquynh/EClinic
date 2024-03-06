using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Paging;
using Project.Common.Response;
using Project.CommunicateService.Queries;
using Project.CommunicateService.Repository.RoomRepositories;
using Project.Core.Logger;

namespace Project.CommunicateService.Handlers.ChatMessageHandlers
{
    public class GetAllImageOfRoomHandler : IRequestHandler<GetAllImageOfRoomQuery, ObjectResult>
    {
        private readonly IRoomRepository roomRepository;
        private readonly ILogger<GetAllImageOfRoomHandler> logger;

        public GetAllImageOfRoomHandler(IRoomRepository roomRepository, ILogger<GetAllImageOfRoomHandler> logger)
        {
            this.roomRepository = roomRepository;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetAllImageOfRoomQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Room = await roomRepository.GetRoom(request.RoomID);
                if (Room == null)
                {
                    return ApiResponse.NotFound("Room Not Found");
                }
                var ChatMessages = Room.ChatMessages;
                ChatMessages = ChatMessages.OrderByDescending(x => x.CreatedAt).ToList();
                List<string> images = ChatMessages.Where(x => x.Type == Data.MessageType.Image).Select(x => x.Content).ToList();
                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = ChatMessages.Count;
                ChatMessages = ChatMessages
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();
                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;
                return ApiResponse.OK<List<string>>(images);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
