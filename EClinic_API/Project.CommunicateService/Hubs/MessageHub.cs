using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Project.CommunicateService.Hubs
{
    public class MessageHub : Hub
    {
        [Authorize]
        public async Task JoinGroup(Guid RoomID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, RoomID.ToString());
        }
        [Authorize]
        public async Task LeaveGroup(Guid RoomID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, RoomID.ToString());
        }
    }
}
