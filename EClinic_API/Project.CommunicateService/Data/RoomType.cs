namespace Project.CommunicateService.Data
{
    public class RoomType
    {
        public Guid RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
