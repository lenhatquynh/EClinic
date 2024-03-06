using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Enum;
using Project.Common.Functionality;
using Project.Common.Model;
using Project.Common.Response;
using Project.Core.Logger;
using Project.PaymentService.Queries;
using Project.PaymentService.Repository.PaymentRepositories;

namespace Project.PaymentService.Handlers.PaymentHandlers
{
    public class GetStatisticsOverviewHandler : IRequestHandler<GetStatisticsOverviewQuery, ObjectResult>
    {
        private readonly ILogger<GetStatisticsOverviewHandler> logger;
        private readonly IPaymentRepository paymentRepository;

        public GetStatisticsOverviewHandler(ILogger<GetStatisticsOverviewHandler> logger, IPaymentRepository paymentRepository)
        {
            this.logger = logger;
            this.paymentRepository = paymentRepository;
        }

        public async Task<ObjectResult> Handle(GetStatisticsOverviewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var CurrentTime = DateTime.Now;
                var OldTime = TimeCalculation.GetOldTime(CurrentTime);
                var paymentsCurrent = await paymentRepository.GetAllAsync(x => x.PaymentTime.Month == CurrentTime.Month && x.PaymentTime.Year == CurrentTime.Year);
                var paymentsOld = await paymentRepository.GetAllAsync(x => x.PaymentTime.Month == OldTime.Month && x.PaymentTime.Year == OldTime.Year);
                var Current = paymentsCurrent.Sum(x => x.PaymentAmount);
                var Old = paymentsOld.Sum(x => x.PaymentAmount);
                double difference = Old - Current;
                double percentageChange = Math.Abs(Old) != 0 ? (double)(difference) / Math.Abs(Old) * 100 : Math.Abs(Current);
                StatisticsStatus status = StatisticsStatus.Equal;
                if (percentageChange > 0)
                {
                    status = StatisticsStatus.Increase;
                }
                else if (percentageChange < 0)
                {
                    status = StatisticsStatus.Decrease;
                }
                else if (percentageChange == 0)
                {
                    status = StatisticsStatus.Equal;
                }

                StatisticsOverviewDtos statistics = new StatisticsOverviewDtos
                {
                    Percent = Math.Abs(Math.Round(percentageChange, 1)),
                    Status = status,
                    Total = (int)Current
                };
                return ApiResponse.OK(statistics);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
