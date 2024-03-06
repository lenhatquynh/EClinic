using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.PaymentService.Helper;
using Project.PaymentService.Model;
using Project.PaymentService.Queries;
using Project.PaymentService.Repository.PaymentRepositories;

namespace Project.PaymentService.Handlers.PaymentHandlers
{
    public class GetTransactionHandler : IRequestHandler<GetTransactionQuery, ObjectResult>
    {
        private readonly ILogger<GetTransactionHandler> logger;
        private readonly IPaymentRepository paymentRepository;

        public GetTransactionHandler(ILogger<GetTransactionHandler> logger, IPaymentRepository paymentRepository)
        {
            this.logger = logger;
            this.paymentRepository = paymentRepository;
        }

        public async Task<ObjectResult> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var timeType = request.TransactionQueryModel.TimeType;
                var startTime = request.TransactionQueryModel.StartTime;
                var endTime = request.TransactionQueryModel.EndTime;
                if (!DataHelper.CheckTimeType(startTime, endTime, timeType))
                {
                    return ApiResponse.BadRequest("Start time and end time incorect");
                }
                List<TransactionDtos> TransactionDtos = new List<TransactionDtos>();
                if (timeType == TimeType.Day)
                {
                    for (DateTime i = startTime; i <= endTime; i = i.AddDays(1))
                    {
                        var payments = await paymentRepository.GetAllAsync(x => x.PaymentTime.Month == i.Month
                                                         && x.PaymentTime.Year == i.Year && x.PaymentTime.Day == i.Day);
                        var totalAmount = payments.Sum(x => x.PaymentAmount);
                        TransactionDtos.Add(new Model.TransactionDtos
                        {
                            Time = i,
                            TotalAmount = totalAmount
                        });
                    }
                }
                if (timeType == TimeType.Month)
                {
                    for (int i = startTime.Month; i <= endTime.Month; i++)
                    {
                        DateTime currentDate = new DateTime(startTime.Year, i, startTime.Day);
                        var payments = await paymentRepository.GetAllAsync(x => x.PaymentTime.Month == currentDate.Month
                                                         && x.PaymentTime.Year == currentDate.Year);
                        var totalAmount = payments.Sum(x => x.PaymentAmount);
                        TransactionDtos.Add(new Model.TransactionDtos
                        {
                            Time = currentDate,
                            TotalAmount = totalAmount
                        });
                    }
                }
                if (timeType == TimeType.Year)
                {
                    for (int i = startTime.Year; i <= endTime.Year; i++)
                    {
                        DateTime currentDate = new DateTime(i, startTime.Month, startTime.Day);
                        var payments = await paymentRepository.GetAllAsync(x => x.PaymentTime.Year == currentDate.Year);
                        var totalAmount = payments.Sum(x => x.PaymentAmount);
                        TransactionDtos.Add(new Model.TransactionDtos
                        {
                            Time = currentDate,
                            TotalAmount = totalAmount
                        });
                    }
                }
                return ApiResponse.OK<List<TransactionDtos>>(TransactionDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
