using MassTransit.Internals;
using Microsoft.EntityFrameworkCore;
using Project.Common.Constants;
using Project.Common.Paging;
using Project.Data.Repository.MSSQL;
using Project.IdentityService.Data;
using Project.IdentityService.Data.Configurations;
using Project.IdentityService.Dtos;

namespace Project.IdentityService.Repository.UserRepository
{
    public class UserRepository : MSSQLRepository<ApplicationDbContext, User>, IUserRepository
    {
        private readonly ApplicationDbContext context;
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            context = dbContext;
        }

        public async Task<PaginationModel<List<User>>> GetUsersAsync(PaginationRequestHeader pagination, SearchUserDtos searchUserDtos)
        {
            var users = new List<User>();
            var usersQuery = context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchUserDtos.UserName))
            {
                usersQuery = usersQuery.Where(u => u.UserName.ToLower().Contains(searchUserDtos.UserName.ToLower()));
            }

            if (searchUserDtos.Enabled != null)
            {
                usersQuery = usersQuery.Where(u => u.Enabled == searchUserDtos.Enabled);
            }

            usersQuery = usersQuery.Where(u =>
                u.CreatedAt >= searchUserDtos.CreateTimeFrom &&
                u.CreatedAt <= searchUserDtos.CreateTimeTo &&
                u.UpdatedAt >= searchUserDtos.UpdateTimeFrom &&
                u.UpdatedAt <= searchUserDtos.UpdateTimeTo);

            if (!string.IsNullOrEmpty(searchUserDtos.Role))
            {
                switch (searchUserDtos.Role)
                {
                    case RoleConstants.Admin:
                        usersQuery = usersQuery.Where(u => u.RoleID == RoleConstants.IDAdmin);
                        break;
                    case RoleConstants.Supporter:
                        usersQuery = usersQuery.Where(u => u.RoleID == RoleConstants.IDSupporter);
                        break;
                    case RoleConstants.Doctor:
                        usersQuery = usersQuery.Where(u => u.RoleID == RoleConstants.IDDoctor);
                        break;
                    case RoleConstants.User:
                        usersQuery = usersQuery.Where(u => u.RoleID == RoleConstants.IDUser);
                        break;
                    case RoleConstants.Expert:
                        usersQuery = usersQuery.Where(u => u.RoleID == RoleConstants.IDExpert);
                        break;
                }
            }
            if (searchUserDtos.IsSortAscending != null)
            {
                if (searchUserDtos.IsSortAscending == true)
                {
                    users = await usersQuery
                        .OrderBy(u => u.CreatedAt)
                        .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                        .Take(pagination.PageSize).ToListAsync();
                }
                else
                {
                    users = await usersQuery
                       .OrderByDescending(u => u.CreatedAt)
                       .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                       .Take(pagination.PageSize).ToListAsync();
                }
            }
            else
            {
                users = await usersQuery.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
            }

            var paginationResponseHeader = new PaginationResponseHeader
            {
                PageSize = pagination.PageSize,
                PageIndex = pagination.PageNumber,
                TotalCount = usersQuery.Count()
            };
            return new PaginationModel<List<User>> { PaginationData = users, PaginationResponseHeader = paginationResponseHeader };
        }
    }
}

