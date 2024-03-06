using MediatR;
using Project.CommunicateService.Commands;
using Project.CommunicateService.Data;
using Project.CommunicateService.Data.Configurations;
using Project.CommunicateService.Repository.RoomRepositories;
using Project.Core.Logger;

namespace Project.CommunicateService.Handlers.RoomHandlers
{
    public class CreateDoctorRoomHandler : IRequestHandler<CreateDoctorRoomCommand, Room>
    {
        private readonly IRoomRepository roomRepository;
        private readonly ILogger<CreateDoctorRoomHandler> logger;

        public CreateDoctorRoomHandler(IRoomRepository roomRepository, ILogger<CreateDoctorRoomHandler> logger)
        {
            this.roomRepository = roomRepository;
            this.logger = logger;
        }

        public async Task<Room> Handle(CreateDoctorRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Room = new Room
                {
                    RoomTypeID = ConstantsData.DoctorRoomTypeID,
                    ReceiverID = Guid.Parse(request.DoctorID),
                    SenderID = Guid.Parse(request.UserID)
                };
                var result = await roomRepository.CreateEntityAsync(Room);
                if (result == null)
                {
                    throw new Exception("Create Room Error.");
                }
                return Room;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }
    }
}
