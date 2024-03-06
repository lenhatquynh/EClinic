using Project.CommunicateService.Data;
using Project.Data.Repository.MSSQL;
using System.Linq.Expressions;

namespace Project.CommunicateService.Repository.ChatMessageRepositories
{
    public interface IChatMessageRepository : IMSSQLRepository<ChatMessage>
    {
        Task<List<ChatMessage>> GetAllMessageOfRoom(Guid UserID);
    }
}
