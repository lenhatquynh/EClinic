using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Project.CommunicateService.Hubs
{
    public class CallHub : Hub
    {
        [Authorize]
        public async Task JoinCall(Guid RoomID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, RoomID.ToString() + "Call");
        }
        [Authorize]
        public async Task CallUser(string signal, string data, Guid RoomID)
        {
            await Clients.Group(RoomID.ToString() + "Call").SendAsync("CallUser", signal, data);
        }
        [Authorize] 
        public async Task AnswerCall(string signal, string data, Guid RoomID)
        {
            await Clients.Group(RoomID.ToString() + "Call").SendAsync("AnswerCall", signal, data);
        }
        [Authorize]
        public async Task Disconnect(Guid RoomID)
        {
            await Clients.Group(RoomID.ToString() + "Call").SendAsync("Disconnect");
        }
        [Authorize]
        public async Task LeaveGroup(Guid RoomID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, RoomID.ToString());
        }
    }
}
