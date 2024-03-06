using AutoMapper;
using MediatR;
using Project.BookingService.Data;
using Project.BookingService.Repository.BookingPackageRepository;
using Project.BookingServiceCommands.Commands;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.BookingPackageHandler;

public class CreateBookingPackageHandler : IRequestHandler<CreateBookingPackageCommand, BookingPackage>
{
    private readonly ILogger<CreateBookingPackageHandler> logger;
    private readonly IBookingPackageRepository repository;
    private readonly IMapper mapper;

    public CreateBookingPackageHandler(IBookingPackageRepository repository, ILogger<CreateBookingPackageHandler> logger, IMapper mapper)
    {
        this.repository = repository;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<BookingPackage> Handle(CreateBookingPackageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bookings = await repository.GetAllAsync(x => x.BookingStatus == BookingStatus.NoPayment);
            if (bookings.Count != 0)
            {
                await repository.DeleteRangeAsync(bookings);
            }
            BookingPackage bookingPackage = mapper.Map<BookingPackage>(request.CreateBookingPackageDTO);

            bookingPackage.BookingStatus = BookingStatus.NoPayment;
            bookingPackage.BookingTime = DateTime.Now;

            return await repository.CreateEntityAsync(bookingPackage);
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return null;
        }

    }
}
