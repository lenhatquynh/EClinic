using MediatR;
using Project.BookingService.Data;
using Project.BookingService.Repository.BookingDoctorRepository;
using Project.BookingServiceQueries.Queries;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.BookingDoctorHandler
{
    public class GetBookingDoctorHandler : IRequestHandler<GetBookingDoctorQuery, BookingDoctor>
    {
        private readonly IBookingDoctorRepository repository;
        private readonly ILogger<GetBookingDoctorHandler> logger;

        public GetBookingDoctorHandler(IBookingDoctorRepository repository, ILogger<GetBookingDoctorHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        public async Task<BookingDoctor> Handle(GetBookingDoctorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var BookingDoctor = await repository.GetBookingDoctor(x => x.BookingID == request.BookingDoctorID);
                return BookingDoctor;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }
    }
}
