namespace Project.CommunicateService.Dtos.ChatMessageDtos
{
    public class CreateMessageFileDtos
    {
        public IFormFile File { get; set; }
        public Guid RoomID { get; set; }
    }
}
