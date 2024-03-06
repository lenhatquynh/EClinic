using Grpc.Net.Client;
using MediatR;
using Project.BookingService.Data;
using Project.BookingService.Protos;
using Project.BookingService.Repository.BookingDoctorRepository;
using Project.BookingServiceCommands.Commands;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.BookingDoctorHandler;

public class UpdateBookingUpcomingForBookingDoctorHandler : IRequestHandler<UpdateBookingUpcomingForBookingDoctorCommand, BookingDoctor>
{
    private readonly ILogger<UpdateBookingUpcomingForBookingDoctorHandler> logger;
    private readonly IBookingDoctorRepository repository;
    private readonly CommunicationService.CommunicationServiceClient client;
    public UpdateBookingUpcomingForBookingDoctorHandler(IConfiguration configuration, ILogger<UpdateBookingUpcomingForBookingDoctorHandler> logger, IBookingDoctorRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:CommunicationServiceURL"), new GrpcChannelOptions { HttpHandler = httpHandler });
        client = new CommunicationService.CommunicationServiceClient(channel);
    }

    public async Task<BookingDoctor> Handle(UpdateBookingUpcomingForBookingDoctorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bookingDoctor = await repository.GetAsync(request.BookingDoctorID);

            if (bookingDoctor is null)
            {
                return null;
            }
            if (bookingDoctor.BookingType == BookingType.Offline)
            {
                bookingDoctor.RoomID = Guid.Empty;
            }
            if (bookingDoctor.BookingType == BookingType.Online)
            {
                var res = await client.CreateRoomAsync(new CreateRoomRequest
                {
                    UserID = bookingDoctor.UserID.ToString(),
                    DoctorID = bookingDoctor.DoctorID.ToString()
                });
                if (res == null) { return null; }
                bookingDoctor.RoomID = Guid.Parse(res.RoomID);
            }
            bookingDoctor.BookingStatus = BookingStatus.Upcoming;

            await repository.UpdateAsync(bookingDoctor);

            return bookingDoctor;
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return null;
        }

    }
}
