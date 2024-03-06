using Grpc.Core;
using MediatR;
using Project.BookingService.Data;
using Project.BookingService.Dtos.BookingDoctorDtos;
using Project.BookingService.Dtos.BookingPackageDTOs;
using Project.BookingService.Protos;
using Project.BookingServiceCommands.Commands;
using Project.BookingServiceQueries.Queries;
using Project.Core.Logger;

namespace Project.BookingService.Service
{
    public class BookingDataService : Protos.BookingService.BookingServiceBase
    {
        private IMediator mediator;
        private ILogger<BookingDataService> logger;

        public BookingDataService(IMediator mediator, ILogger<BookingDataService> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public override async Task<CreateBookingDoctorResponse> CreateBookingDoctor(CreateBookingDoctorRequest request, ServerCallContext context)
        {
            try
            {
                var result = await mediator.Send(new CreateBookingDoctorCommand(new CreateBookingDoctorDto
                {
                    ProfileID = Guid.Parse(request.ProfileID),
                    Price = request.Price,
                    BookingType = (BookingType)request.BookingType,
                    DoctorID = Guid.Parse(request.DoctorID),
                    UserID = Guid.Parse(request.UserID),
                    ScheduleID = Guid.Parse(request.ScheduleID)
                }));
                if (result == null) { return null; }
                return new CreateBookingDoctorResponse { BookingDoctorID = result.BookingID.ToString() };
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public override async Task<CreateBookingPackageResponse> CreateBookingPackage(CreateBookingPackageRequest request, ServerCallContext context)
        {
            try
            {
                var result = await mediator.Send(new CreateBookingPackageCommand(new CreateBookingPackageDTO
                {
                    ProfileID = Guid.Parse(request.ProfileID),
                    Price = request.Price,
                    AppoinmentTime = DateTime.Parse(request.AppoinmentTime),
                    ServicePackageID = Guid.Parse(request.ServicePackageID),
                    UserID = Guid.Parse(request.UserID)
                }));
                if (result == null) { return null; }
                return new CreateBookingPackageResponse { BookingPackageID = result.BookingID.ToString() };
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public override async Task<GetBookingResponse> GetBookingDoctor(GetBookingDoctorRequest request, ServerCallContext context)
        {
            try
            {
                var result = await mediator.Send(new GetBookingDoctorQuery(Guid.Parse(request.BookingDoctorID)));
                if (result == null) { return null; }
                return new GetBookingResponse { IsSuccess = true, UserID = result.DoctorID.ToString() };
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public override async Task<GetBookingResponse> GetBookingPackage(GetBookingPackageRequest request, ServerCallContext context)
        {
            try
            {
                var result = await mediator.Send(new GetBookingPackageQuery(Guid.Parse(request.BookingPackageID)));
                if (result == null) { return null; }
                return new GetBookingResponse { IsSuccess = true, UserID = result.ServicePackageID.ToString() };
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public override async Task<UpdateBookingResponse> UpdateBookingDoctor(UpdateBookingDoctorRequest request, ServerCallContext context)
        {
            try
            {
                var result = await mediator.Send(new UpdateBookingUpcomingForBookingDoctorCommand(Guid.Parse(request.BookingDoctorID)));
                if (result == null) { return null; }
                return new UpdateBookingResponse { IsSuccess = true, UserID = result.DoctorID.ToString() };
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public override async Task<UpdateBookingResponse> UpdateBookingPackage(UpdateBookingPackageRequest request, ServerCallContext context)
        {
            try
            {
                var result = await mediator.Send(new UpdateBookingUpcomingForBookingPackageCommand(Guid.Parse(request.BookingPackageID)));
                if (result == null) { return null; }
                return new UpdateBookingResponse { IsSuccess = true, UserID = result.ServicePackageID.ToString() };
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }
    }
}
