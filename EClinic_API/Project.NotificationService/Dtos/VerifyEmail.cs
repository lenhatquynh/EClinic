namespace Project.NotificationService.Dtos
{
    public class VerifyEmail
    {
        public string Email { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
    }
}
