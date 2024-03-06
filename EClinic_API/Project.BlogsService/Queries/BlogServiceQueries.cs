using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BlogService.Dtos.BlogDtos;
using Project.Common.Paging;

namespace Project.BlogService.Queries
{

    public record GetAllBlogQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetBlogQuery(Guid BlogID) : IRequest<ObjectResult>;
    public record GetBlogsQuery(PaginationRequestHeader PaginationRequestHeader, SearchBlogDtos SearchBlogDtos, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetAllBlogForAdQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetBlogForAdQuery(Guid BlogID) : IRequest<ObjectResult>;
    public record GetBlogsForAdQuery(PaginationRequestHeader PaginationRequestHeader, SearchBlogDtos SearchBlogDtos, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetPostOfUserQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, string UserID) : IRequest<ObjectResult>;
    public record GetPostNotActiveQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetPostNoAnswerQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetAnswerQuery(Guid PostID, string UserID) : IRequest<ObjectResult>;
    public record GetAllCommentQuery(Guid PostID, string UserID) : IRequest<ObjectResult>;
    public record GetAllHashtagQuery() : IRequest<ObjectResult>;
    public record GetPostsOfHashTagQuery(PaginationRequestHeader PaginationRequestHeader, string SearchText, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetPostsSortByLikeQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response) : IRequest<ObjectResult>;
    public record GetTagSortByCountQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response) : IRequest<ObjectResult>;
}
