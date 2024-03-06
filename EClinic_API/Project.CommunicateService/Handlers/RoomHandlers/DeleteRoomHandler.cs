using MediatR;
using Project.CommunicateService.Commands;
using Project.CommunicateService.Repository.RoomRepositories;
using Project.Core.Logger;

namespace Project.CommunicateService.Handlers.RoomHandlers
{
    public class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand, bool>
    {
        private readonly IRoomRepository roomRepository;
        private readonly ILogger<DeleteRoomHandler> logger;

        public DeleteRoomHandler(IRoomRepository roomRepository, ILogger<DeleteRoomHandler> logger)
        {
            this.roomRepository = roomRepository;
            this.logger = logger;
        }

        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var room = await roomRepository.GetAsync(request.RoomID);
                return await roomRepository.DeleteAsync(room);

            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return false;
            }
        }
    }
}
