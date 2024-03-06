namespace Project.CommunicateService.Data
{
    public class Room
    {
        public Guid RoomID { get; set; }
        public bool IsClosed { get; set; } = false;
        public Guid SenderID { get; set; }
        public Guid ReceiverID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid RoomTypeID { get; set; }
        public RoomType RoomType { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
