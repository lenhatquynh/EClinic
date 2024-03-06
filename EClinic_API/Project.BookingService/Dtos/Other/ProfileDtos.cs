namespace Project.BookingService.Dtos.Other
{
    public class ProfileDtos
    {
        public Guid ProfileID { get; set; }
        public Guid UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
    }
}
