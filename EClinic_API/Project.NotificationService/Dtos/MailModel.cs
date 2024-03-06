namespace Project.NotificationService.Dtos
{
    public class MailModel
    {
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> CC { get; set; }
        public List<IFormFile> Attachments { get; set; }

    }
}
