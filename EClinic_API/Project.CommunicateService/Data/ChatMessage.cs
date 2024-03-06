namespace Project.CommunicateService.Data
{
    public class ChatMessage
    {
        public Guid ChatMessageID { get; set; }
        public Guid UserID { get; set; }
        public string Content { get; set; }
        public MessageType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid RoomID { get; set; }
        public Room Room { get; set; }
    }
}
