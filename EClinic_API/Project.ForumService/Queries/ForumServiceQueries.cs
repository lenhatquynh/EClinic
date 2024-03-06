using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Paging;
using Project.ForumService.Dtos.PostsDtos;

namespace Project.ForumService.Queries
{

    public record GetAllPostQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, string SearchText) : IRequest<ObjectResult>;
    public record GetPostQuery(Guid PostID, string UserID) : IRequest<ObjectResult>;
    public record GetPostOfUserQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, string UserID) : IRequest<ObjectResult>;
    public record GetPostNotActiveQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response,string SearchText) : IRequest<ObjectResult>;
    public record GetPostNoAnswerQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetAnswerQuery(Guid PostID, string UserID) : IRequest<ObjectResult>;
    public record GetAllCommentQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, Guid PostID, string UserID) : IRequest<ObjectResult>;
    public record GetAllHashtagQuery() : IRequest<ObjectResult>;
    public record GetPostsQuery(PaginationRequestHeader PaginationRequestHeader, SearchPostDtos SearchPostDtos, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetPostsOfHashTagQuery(PaginationRequestHeader PaginationRequestHeader, string SearchText, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetPostsSortByLikeQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetTagSortByCountQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response) : IRequest<ObjectResult>;
}
