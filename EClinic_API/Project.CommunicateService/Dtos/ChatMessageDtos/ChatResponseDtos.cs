using Project.CommunicateService.Dtos.RoomDtos;

namespace Project.CommunicateService.Dtos.ChatMessageDtos
{
    public class ChatResponseDtos
    {
        public Author MyProfile { get; set; }
        public Author OtherProfile { get; set; }
        public List<ChatMessageDto> Message { get; set; }
    }
}
