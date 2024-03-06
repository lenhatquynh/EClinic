using Grpc.Net.Client;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BookingService.Data;
using Project.BookingService.Protos;
using Project.BookingService.Repository.BookingPackageRepository;
using Project.BookingServiceCommands.Commands;
using Project.Common.Constants;
using Project.Common.Model;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Core.RabbitMQ;

namespace Project.BookingService.Handlers.BookingPackageHandler;

public class UpdateBookingStatusForBookingPackageHandler : IRequestHandler<UpdateBookingStatusForBookingPackageCommand, ObjectResult>
{
    private readonly ILogger<UpdateBookingStatusForBookingPackageHandler> logger;
    private readonly IBookingPackageRepository repository;
    private readonly IBus bus;
    private readonly ServiceInformationService.ServiceInformationServiceClient serviceClient;
    public UpdateBookingStatusForBookingPackageHandler(IConfiguration configuration, ILogger<UpdateBookingStatusForBookingPackageHandler> logger, IBookingPackageRepository repository, IBus bus)
    {
        this.logger = logger;
        this.repository = repository;
        this.bus = bus;
        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        GrpcChannel channel2 = GrpcChannel.ForAddress(configuration.GetValue<string>("GrpcSettings:ServiceInformationServiceUrl"), new GrpcChannelOptions { HttpHandler = httpHandler });
        serviceClient = new ServiceInformationService.ServiceInformationServiceClient(channel2);
    }

    public async Task<ObjectResult> Handle(UpdateBookingStatusForBookingPackageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var bookingPackage = await repository.GetAsync(request.BookingPackageID);

            if (bookingPackage is null)
            {
                return ApiResponse.BadRequest("Booking Package not found!");
            }

            bookingPackage.BookingStatus = request.BookingStatus;
            if (bookingPackage.BookingStatus == BookingStatus.Cancel)
            {
                await bus.SendMessageWithExchangeName<RefundEvent>(new RefundEvent
                {
                    BookingID = bookingPackage.BookingID,
                    BookingType = 1,
                    Price = bookingPackage.Price
                }, ExchangeConstants.PaymentService);
            }
            if (bookingPackage.BookingStatus == BookingStatus.Done)
            {
                var serviceRes = await serviceClient.IncreaseOrderAsync(new IncreaseOrderRequest { ServicePackageID = bookingPackage.ServicePackageID.ToString() });
                if (!serviceRes.IsSuccess)
                {
                    return ApiResponse.InternalServerError();
                }
            }
            await repository.UpdateAsync(bookingPackage);

            return ApiResponse.OK("Update Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
