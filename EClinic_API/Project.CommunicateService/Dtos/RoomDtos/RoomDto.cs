using Project.CommunicateService.Dtos.RoomTypeDtos;
using Project.CommunicateService.Dtos.ChatMessageDtos;

namespace Project.CommunicateService.Dtos.RoomDtos
{
    public class RoomDto
    {
        public Guid RoomID { get; set; }
        public bool IsClosed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public RoomTypeDto RoomType { get; set; }
        public Author RoomAuthor { get; set; }
        public ChatMessageDto ChatMessage { get; set; }
    }
}
