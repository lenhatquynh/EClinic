using MediatR;
using Project.BookingService.Data;
using Project.BookingService.Repository.BookingPackageRepository;
using Project.BookingServiceCommands.Commands;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.BookingPackageHandler;

public class UpdateBookingUpcomingForBookingPackageHandler : IRequestHandler<UpdateBookingUpcomingForBookingPackageCommand, BookingPackage>
{
    private readonly ILogger<UpdateBookingUpcomingForBookingPackageHandler> logger;
    private readonly IBookingPackageRepository repository;

    public UpdateBookingUpcomingForBookingPackageHandler(ILogger<UpdateBookingUpcomingForBookingPackageHandler> logger, IBookingPackageRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    public async Task<BookingPackage> Handle(UpdateBookingUpcomingForBookingPackageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bookingPackage = await repository.GetAsync(request.BookingPackageID);

            if (bookingPackage is null)
            {
                return null;
            }

            bookingPackage.BookingStatus = BookingStatus.Upcoming;

            await repository.UpdateAsync(bookingPackage);

            return bookingPackage;
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return null;
        }

    }
}
