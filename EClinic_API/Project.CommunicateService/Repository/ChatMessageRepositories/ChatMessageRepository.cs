using Microsoft.EntityFrameworkCore;
using Project.CommunicateService.Data;
using Project.CommunicateService.Data.Configurations;
using Project.Data.Repository.MSSQL;

namespace Project.CommunicateService.Repository.ChatMessageRepositories
{
    public class ChatMessageRepository : MSSQLRepository<ApplicationDbContext, ChatMessage>, IChatMessageRepository
    {
        private readonly ApplicationDbContext context;
        public ChatMessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<List<ChatMessage>> GetAllMessageOfRoom(Guid UserID)
        {
           return await context.ChatMessages.Where(x => x.UserID == UserID).GroupBy(x => x.RoomID).Select(g => g.First()).ToListAsync();
        }
    }
}
