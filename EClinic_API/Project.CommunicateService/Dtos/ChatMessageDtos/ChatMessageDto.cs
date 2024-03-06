using Project.CommunicateService.Data;
using Project.CommunicateService.Dtos.RoomDtos;

namespace Project.CommunicateService.Dtos.ChatMessageDtos
{
    public class ChatMessageDto
    {
        public Guid ChatMessageID { get; set; }
        public Guid UserID { get; set; }
        public string Content { get; set; }
        public bool IsImage { get; set; }
        public bool IsMyChat { get; set; } = false;
        public DateTime CreatedAt { get; set; }
    }
}
