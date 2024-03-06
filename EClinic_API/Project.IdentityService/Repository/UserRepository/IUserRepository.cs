using Project.Common.Paging;
using Project.Data.Repository.MSSQL;
using Project.IdentityService.Data;
using Project.IdentityService.Dtos;

namespace Project.IdentityService.Repository.UserRepository
{
    public interface IUserRepository : IMSSQLRepository<User>
    {
        Task<PaginationModel<List<User>>> GetUsersAsync(PaginationRequestHeader pagination,SearchUserDtos searchUserDtos);
    }
}
