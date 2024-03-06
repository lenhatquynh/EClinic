using Project.Data.Repository.MSSQL;
using Project.ProfileService.Data;
using Project.ProfileService.Data.Configurations;

namespace Project.ProfileService.Repository.RelationshipRepository
{
    public class RelationshipRepository : MSSQLRepository<ApplicationDbContext, Relationship>, IRelationshipRepository
    {
        public RelationshipRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
