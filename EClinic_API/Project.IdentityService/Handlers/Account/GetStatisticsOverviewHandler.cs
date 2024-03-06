using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Constants;
using Project.Common.Enum;
using Project.Common.Functionality;
using Project.Common.Model;
using Project.Common.Response;
using Project.Core.Logger;
using Project.IdentityService.Queries;
using Project.IdentityService.Repository.UserRepository;

namespace Project.IdentityService.Handlers.Account
{
    public class GetStatisticsOverviewHandler : IRequestHandler<GetStatisticsOverviewQuery, ObjectResult>
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<GetStatisticsOverviewHandler> logger;

        public GetStatisticsOverviewHandler(IUserRepository userRepository, ILogger<GetStatisticsOverviewHandler> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetStatisticsOverviewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var CurrentTime = DateTime.Now;
                var OldTime = TimeCalculation.GetOldTime(CurrentTime);
                var Current = await userRepository.CountAsync(x => x.CreatedAt.Month == CurrentTime.Month && x.CreatedAt.Year == CurrentTime.Year && x.RoleID == RoleConstants.IDUser);
                var Old = await userRepository.CountAsync(x => x.CreatedAt.Month == OldTime.Month && x.CreatedAt.Year == OldTime.Year && x.RoleID == RoleConstants.IDUser);
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

