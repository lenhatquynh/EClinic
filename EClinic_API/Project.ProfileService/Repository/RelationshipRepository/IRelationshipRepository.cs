using Project.Data.Repository.MSSQL;
using Project.ProfileService.Data;

namespace Project.ProfileService.Repository.RelationshipRepository
{
    public interface IRelationshipRepository : IMSSQLRepository<Relationship>
    {
    }
}
