using Project.CommunicateService.Data;
using Project.Data.Repository.MSSQL;
using static Amazon.S3.Util.S3EventNotification;
using System.Linq.Expressions;

namespace Project.CommunicateService.Repository.RoomRepositories
{
    public interface IRoomRepository : IMSSQLRepository<Room>
    {
        Task<List<Room>> GetAllRoom();
        Task<List<Room>> GetAllRoom(Expression<Func<Room, bool>> filters);
        Task<Room> GetRoom(Guid RoomID);
        Task<Room> GetRoom(Expression<Func<Room, bool>> filter);
    }
}
