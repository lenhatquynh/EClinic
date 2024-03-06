using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BookingService.Repository.BookingDoctorRepository;
using Project.BookingService.Repository.BookingPackageRepository;
using Project.BookingServiceQueries.Queries;
using Project.Common.Enum;
using Project.Common.Functionality;
using Project.Common.Model;
using Project.Common.Response;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.BookingHandler
{
    public class GetStatisticsOverviewHandler : IRequestHandler<GetStatisticsOverviewQuery, ObjectResult>
    {
        private readonly ILogger<GetStatisticsOverviewHandler> logger;
        private readonly IBookingDoctorRepository doctorRepository;
        private readonly IBookingPackageRepository packageRepository;

        public GetStatisticsOverviewHandler(ILogger<GetStatisticsOverviewHandler> logger, IBookingDoctorRepository doctorRepository, IBookingPackageRepository packageRepository)
        {
            this.logger = logger;
            this.doctorRepository = doctorRepository;
            this.packageRepository = packageRepository;
        }

        public async Task<ObjectResult> Handle(GetStatisticsOverviewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var CurrentTime = DateTime.Now;
                var OldTime = TimeCalculation.GetOldTime(CurrentTime);
                var CurrentDoctor = await doctorRepository.CountAsync(x => x.BookingTime.Month == CurrentTime.Month && x.BookingTime.Year == CurrentTime.Year);
                var CurrentPackage = await packageRepository.CountAsync(x => x.BookingTime.Month == CurrentTime.Month && x.BookingTime.Year == CurrentTime.Year);
                int Current = CurrentDoctor + CurrentPackage;
                var OldDoctor = await doctorRepository.CountAsync(x => x.BookingTime.Month == OldTime.Month && x.BookingTime.Year == OldTime.Year);
                var OldPackage = await doctorRepository.CountAsync(x => x.BookingTime.Month == OldTime.Month && x.BookingTime.Year == OldTime.Year);
                int Old = OldDoctor + OldPackage;
                int difference = Old - Current;
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
                    Total = Current
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
