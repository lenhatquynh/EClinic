using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Linq;
using Project.BookingService.Dtos.DoctorScheduleDtos;
using Project.BookingService.Repository.DoctorCalendarRepository;
using Project.BookingServiceQueries.Queries;
using Project.Common.Response;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.DoctorScheduleHandler
{
    public class GetDoctorScheduleByDayForUserHandler : IRequestHandler<GetDoctorScheduleByDayForUserQuery, ObjectResult>
    {
        private readonly IMapper mapper;
        private readonly IDoctorCalendarRepository repository;
        private readonly ILogger<GetDoctorScheduleByDayForUserHandler> logger;

        public GetDoctorScheduleByDayForUserHandler(IMapper mapper, IDoctorCalendarRepository repository, ILogger<GetDoctorScheduleByDayForUserHandler> logger)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetDoctorScheduleByDayForUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Calendar = await repository.GetDoctorCalendarForUserAsync(x => x.Time.Date == request.Date && x.DoctorID == request.DoctorID);
                if (Calendar == null)
                {
                    return ApiResponse.NotFound("Calendar Not Found");
                }
                DoctorScheduleDtos scheduleDtos = new DoctorScheduleDtos();
                scheduleDtos.CalenderID = Calendar.CalenderID;
                scheduleDtos.Time = Calendar.Time;
                var Schedules = Calendar.DoctorSchedules;
                foreach (var schedule in Schedules)
                {
                    if (schedule.BookingDoctor != null && schedule.BookingDoctor.BookingStatus == Data.BookingStatus.NoPayment)
                    {
                        schedule.BookingDoctor = null;
                    }
                }
                Schedules = Schedules.OrderBy(x => x.StartTime).ToList();
                scheduleDtos.Slots = mapper.Map<List<SlotDtos>>(Schedules);
                return ApiResponse.OK(scheduleDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
