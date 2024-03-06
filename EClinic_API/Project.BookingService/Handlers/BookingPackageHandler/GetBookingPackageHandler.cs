using MediatR;
using Project.BookingService.Data;
using Project.BookingService.Repository.BookingPackageRepository;
using Project.BookingServiceQueries.Queries;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.BookingPackageHandler            
{
    public class GetBookingPackageHandler : IRequestHandler<GetBookingPackageQuery, BookingPackage>
    {
        private readonly IBookingPackageRepository repository;
        private readonly ILogger<GetBookingPackageHandler> logger;

        public GetBookingPackageHandler(IBookingPackageRepository repository, ILogger<GetBookingPackageHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<BookingPackage> Handle(GetBookingPackageQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingPackage = await repository.GetAsync(x => x.BookingID == request.BookingPackageID);

                return bookingPackage;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }
    }
}
