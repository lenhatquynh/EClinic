using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.PaymentService.Commands;
using Project.PaymentService.Data;
using Project.PaymentService.Model;
using Project.PaymentService.MomoPayment;
using Project.PaymentService.MomoPayment.MomoPaymentModel;
using Project.PaymentService.Repository.PaymentRepositories;
using Project.PaymentService.Repository.RefundRepositories;
using Project.PaymentService.VNPayPayment;

namespace Project.PaymentService.Handlers.RefundHandlers
{
    public class CreateRefundHandler : IRequestHandler<CreateRefundCommand, ObjectResult>
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IRefundRepository refundRepository;
        private readonly IMomoPayment momoPayment;
        private readonly IVNPayPayment vNPayPayment;
        private readonly ILogger<CreateRefundHandler> logger;

        public CreateRefundHandler(IPaymentRepository paymentRepository, IRefundRepository refundRepository, IMomoPayment momoPayment, IVNPayPayment vNPayPayment, ILogger<CreateRefundHandler> logger)
        {
            this.paymentRepository = paymentRepository;
            this.refundRepository = refundRepository;
            this.momoPayment = momoPayment;
            this.vNPayPayment = vNPayPayment;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(CreateRefundCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var payment = await paymentRepository.GetPayment(request.RefundModel.PaymentID);
                if (payment == null)
                {
                    return ApiResponse.NotFound("Payment not found");
                }
                if (payment.Refund is not null)
                {
                    return ApiResponse.BadRequest("This payment has been refunded");
                }
                Refund refund = new Refund
                {
                    PaymentID = payment.PaymentID,
                    RefundAmount = payment.PaymentAmount,
                    RefundReason = request.RefundModel.Reason,
                    RefundTime = DateTime.Now
                };
                RefundResult result = null;
                switch (payment.PaymentService)
                {

                    case Data.PaymentService.Momo:
                        MomoRefundModel momoRefundModel = new MomoRefundModel
                        {
                            BookingID = payment.BookingID,
                            Amount = payment.PaymentAmount,
                            Message = $"Refund for {payment.BookingID}",
                            TransactionID = payment.TransactionID,
                            UserID = payment.UserID,
                            OrderID = payment.OrderID,
                        };
                        result = await momoPayment.PaymentRefund(momoRefundModel);
                        break;

                    case Data.PaymentService.VNPay:
                        VNPayRefundModel vNPayRefundModel = new VNPayRefundModel
                        {
                            Amount = payment.PaymentAmount,
                            Message = $"Refund for {payment.BookingID}",
                            TransactionID = payment.TransactionID,
                            UserID = payment.UserID,
                            TransactionDate = payment.PaymentTime,
                            OrderID = payment.OrderID
                        };
                        result = await vNPayPayment.PaymentRefund(vNPayRefundModel, request.ipAddress);
                        break;
                    default:
                        result = null;
                        break;
                }
                if (result == null)
                {
                    return ApiResponse.InternalServerError();
                }
                if (!result.IsSuccess)
                {
                    return ApiResponse.InternalServerError();
                }
                await refundRepository.CreateAsync(refund);
                return ApiResponse.Created("Refund Success");
            } 
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
