using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Paging;

namespace Project.CommunicateService.Queries
{
    public record GetAllMessageOfRoomQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, Guid RoomID, string UserID) : IRequest<ObjectResult>;
    public record GetAllImageOfRoomQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, Guid RoomID) : IRequest<ObjectResult>;
    public record GetAllRoomQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, string UserID) : IRequest<ObjectResult>;
    public record GetAllRoomOfSupporterQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, string UserID) : IRequest<ObjectResult>;
    public record GetAllRoomOfDoctorQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, string UserID) : IRequest<ObjectResult>;
    public record GetAllNewRoomQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response) : IRequest<ObjectResult>;
    public record SearchRoomQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, string UserID, string SearchText) : IRequest<ObjectResult>;
    public record GetAllRoomTypeQuery() : IRequest<ObjectResult>;
}
