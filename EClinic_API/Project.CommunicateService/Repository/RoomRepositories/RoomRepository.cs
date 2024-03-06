using Microsoft.EntityFrameworkCore;
using Project.CommunicateService.Data;
using Project.CommunicateService.Data.Configurations;
using Project.Data.Repository.MSSQL;
using System.Linq.Expressions;

namespace Project.CommunicateService.Repository.RoomRepositories
{
    public class RoomRepository : MSSQLRepository<ApplicationDbContext, Room>, IRoomRepository
    {
        private readonly ApplicationDbContext context;
        public RoomRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<List<Room>> GetAllRoom()
        {
            return await context.Rooms.Include(x => x.ChatMessages).Include(x => x.RoomType).ToListAsync();
        }

        public async Task<List<Room>> GetAllRoom(Expression<Func<Room, bool>> filters)
        {
            return await context.Rooms.Include(x => x.ChatMessages).Include(x => x.RoomType).Where(filters).ToListAsync();
        }

        public async Task<Room> GetRoom(Guid RoomID)
        {
            return await context.Rooms.Include(x => x.ChatMessages).SingleOrDefaultAsync(x => x.RoomID == RoomID);
        }

        public async Task<Room> GetRoom(Expression<Func<Room, bool>> filter)
        {
            return await context.Rooms.Include(x => x.ChatMessages).SingleOrDefaultAsync(filter);
        }
    }
}
