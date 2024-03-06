using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.CommunicateService.Commands;
using Project.CommunicateService.Data;
using Project.CommunicateService.Data.Configurations;
using Project.CommunicateService.Dtos.ChatMessageDtos;
using Project.CommunicateService.Repository.RoomRepositories;
using Project.Core.Logger;

namespace Project.CommunicateService.Handlers.RoomHandlers
{
    public class CreateSupporterRoomHandler : IRequestHandler<CreateSupporterRoomCommand, ObjectResult>
    {
        private readonly IRoomRepository roomRepository;
        private readonly ILogger<CreateSupporterRoomHandler> logger;
        private readonly IMediator mediator;

        public CreateSupporterRoomHandler(IRoomRepository roomRepository, ILogger<CreateSupporterRoomHandler> logger, IMediator mediator)
        {
            this.roomRepository = roomRepository;
            this.logger = logger;
            this.mediator = mediator;
        }

        public async Task<ObjectResult> Handle(CreateSupporterRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Room = new Room
                {
                    RoomTypeID = ConstantsData.SupporterRoomTypeID,
                    ReceiverID = Guid.Empty,
                    SenderID = Guid.Parse(request.UserID)
                };
                var result = await roomRepository.CreateEntityAsync(Room);
                if (result == null)
                {
                    throw new Exception("Create Room Error.");
                }
                await mediator.Send(new CreateMessageCommand(request.UserID, new CreateMessageDtos { Content = request.Message, RoomID = result.RoomID }));
                return ApiResponse.Created<Guid>(result.RoomID);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
