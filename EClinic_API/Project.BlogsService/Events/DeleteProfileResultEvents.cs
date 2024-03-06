namespace Project.BlogService.Events
{
    public class DeleteProfileResultEvents
    {
        public bool IsSuccess { get; set; }
        public Guid UserID { get; set; }
    }
}
