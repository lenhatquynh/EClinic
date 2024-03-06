using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Paging;
using Project.IdentityService.Data;
using Project.IdentityService.Dtos;

namespace Project.IdentityService.Queries
{
    public record GetAllUserQuery(PaginationRequestHeader PaginationRequestHeader, SearchUserDtos SearchUserDtos, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetAllRoleQuery() : IRequest<ObjectResult>;
    public record GetUserEnabledQuery(Guid userID) : IRequest<int>;
    public record GetAllUserWithRoleQuery(string Role) : IRequest<List<User>>;
    public record GetStatisticsOverviewQuery() : IRequest<ObjectResult>;
}
