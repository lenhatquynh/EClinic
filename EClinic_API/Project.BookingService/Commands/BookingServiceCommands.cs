using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BookingService.Data;
using Project.BookingService.Dtos.BookingDoctorDtos;
using Project.BookingService.Dtos.BookingPackageDTOs;
using Project.BookingService.Dtos.DoctorScheduleDtos;

namespace Project.BookingServiceCommands.Commands
{
    #region BookingPackage
    public record CreateBookingPackageCommand(CreateBookingPackageDTO CreateBookingPackageDTO) : IRequest<BookingPackage>;
    public record UpdateBookingStatusForBookingPackageCommand(Guid BookingPackageID, BookingStatus BookingStatus) : IRequest<ObjectResult>;
    public record UpdateBookingUpcomingForBookingPackageCommand(Guid BookingPackageID) : IRequest<BookingPackage>;
    #endregion
    #region BookingDoctor
    public record CreateBookingDoctorCommand(CreateBookingDoctorDto CreateBookingDoctorDTO) : IRequest<BookingDoctor>;
    public record UpdateBookingStatusForBookingDoctorCommand(Guid BookingDoctorID, BookingStatus BookingStatus) : IRequest<ObjectResult>;
    public record UpdateBookingUpcomingForBookingDoctorCommand(Guid BookingDoctorID) : IRequest<BookingDoctor>;
    #endregion
    #region DoctorSchedule
    public record CreateDoctorScheduleCommand(CreateDoctorScheduleDtos CreateDoctorScheduleDtos) : IRequest<ObjectResult>;
    public record UpdateDoctorScheduleCommand(UpdateDoctorScheduleDtos UpdateDoctorScheduleDtos) : IRequest<ObjectResult>;
    #endregion
}