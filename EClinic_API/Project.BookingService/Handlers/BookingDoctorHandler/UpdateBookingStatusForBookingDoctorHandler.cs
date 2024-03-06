using Grpc.Net.Client;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BookingService.Data;
using Project.BookingService.Protos;
using Project.BookingService.Repository.BookingDoctorRepository;
using Project.BookingServiceCommands.Commands;
using Project.Common.Constants;
using Project.Common.Model;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Core.RabbitMQ;

namespace Project.BookingService.Handlers.BookingPackageHandler;

public class UpdateBookingStatusForBookingDoctorHandler : IRequestHandler<UpdateBookingStatusForBookingDoctorCommand, ObjectResult>
{
    private readonly ILogger<UpdateBookingStatusForBookingDoctorHandler> logger;
    private readonly IBookingDoctorRepository repository;
    private readonly IBus bus;
    private readonly CommunicationService.CommunicationServiceClient client;
    public UpdateBookingStatusForBookingDoctorHandler(IConfiguration configuration, ILogger<UpdateBookingStatusForBookingDoctorHandler> logger, IBookingDoctorRepository repository, IBus bus)
    {
        this.logger = logger;
        this.repository = repository;
        this.bus = bus;
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        GrpcChannel channel = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:CommunicationServiceURL"), new GrpcChannelOptions { HttpHandler = httpHandler });
        client = new CommunicationService.CommunicationServiceClient(channel);
    }

    public async Task<ObjectResult> Handle(UpdateBookingStatusForBookingDoctorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bookingDoctor = await repository.GetAsync(request.BookingDoctorID);

            if (bookingDoctor is null)
            {
                return ApiResponse.BadRequest("Booking Doctor not found!");
            }

            bookingDoctor.BookingStatus = request.BookingStatus;
            if (bookingDoctor.BookingStatus == BookingStatus.Cancel)
            {
                if (bookingDoctor.RoomID != Guid.Empty)
                {
                    await client.DeleteRoomAsync(new DeleteRoomRequest
                    {
                        RoomID = bookingDoctor.RoomID.ToString()
                    });
                }
                await bus.SendMessageWithExchangeName<RefundEvent>(new RefundEvent
                {
                    BookingID = bookingDoctor.BookingID,
                    BookingType = 0,
                    Price = bookingDoctor.Price
                }, ExchangeConstants.PaymentService);
            }
            await repository.UpdateAsync(bookingDoctor);

            return ApiResponse.OK("Update Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
