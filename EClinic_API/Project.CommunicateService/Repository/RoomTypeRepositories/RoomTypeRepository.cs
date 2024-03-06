using Project.CommunicateService.Data;
using Project.CommunicateService.Data.Configurations;
using Project.Data.Repository.MSSQL;

namespace Project.CommunicateService.Repository.RoomTypeRepositories
{
    public class RoomTypeRepository : MSSQLRepository<ApplicationDbContext, RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
