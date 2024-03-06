namespace Project.ForumService.Events
{
    public class DeleteProfileResultEvents
    {
        public bool IsSuccess { get; set; }
        public Guid UserID { get; set; }
    }
}
