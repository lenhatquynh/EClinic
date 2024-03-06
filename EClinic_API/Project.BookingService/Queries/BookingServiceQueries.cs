using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BookingService.Data;
using Project.Common.Paging;

namespace Project.BookingServiceQueries.Queries
{
    #region Booking Package
    public record GetBookingPackageByIDQuery(Guid BookingPackageID) : IRequest<ObjectResult>;
    public record GetBookingPackageQuery(Guid BookingPackageID) : IRequest<BookingPackage>;
    public record GetAllBookingPackageForAdQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response,BookingStatus BookingStatus) : IRequest<ObjectResult>;
    public record GetAllBookingPackageForUserQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, string UserID, BookingStatus BookingStatus) : IRequest<ObjectResult>;
    #endregion
    #region Booking Doctor
    public record GetBookingDoctorByIDQuery(Guid BookingDoctorID) : IRequest<ObjectResult>;
    public record GetBookingDoctorQuery(Guid BookingDoctorID) : IRequest<BookingDoctor>;
    public record GetAllBookingDoctorForAdQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, BookingStatus BookingStatus) : IRequest<ObjectResult>;
    public record GetAllBookingDoctorForDoctorQuery(string userID,PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, BookingStatus BookingStatus) : IRequest<ObjectResult>;
    public record GetAllBookingDoctorForUserQuery(PaginationRequestHeader PaginationRequestHeader, HttpResponse Response, string UserID, BookingStatus BookingStatus) : IRequest<ObjectResult>;
    #endregion
    #region  Doctor Schedule
    public record GetDoctorScheduleByDayForAdQuery(DateTime Date, Guid DoctorID) : IRequest<ObjectResult>;
    public record GetDoctorScheduleByDayForUserQuery(DateTime Date, Guid DoctorID) : IRequest<ObjectResult>;
    #endregion
    public record GetStatisticsOverviewQuery() : IRequest<ObjectResult>;
}
