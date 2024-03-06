using Project.Data.Model;

namespace Project.BlogService.Data;

public class Hashtag : MongoDBEntity
{
    public Guid Id { get; set; }
    public string HashtagName { get; set; }
    public int Count { get; set; }
}