using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BookingService.Data;
using Project.BookingService.Repository.DoctorCalendarRepository;
using Project.BookingService.Repository.DoctorScheduleRepository;
using Project.BookingServiceCommands.Commands;
using Project.Common.Response;
using Project.Core.Logger;

namespace Project.BookingService.Handlers.DoctorScheduleHandler
{
    public class CreateDoctorScheduleHandler : IRequestHandler<CreateDoctorScheduleCommand, ObjectResult>
    {
        private readonly IDoctorCalendarRepository calendarRepository;
        private readonly IDoctorScheduleRepository scheduleRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateDoctorScheduleHandler> logger;

        public CreateDoctorScheduleHandler(IDoctorCalendarRepository calendarRepository, IDoctorScheduleRepository scheduleRepository, IMapper mapper, ILogger<CreateDoctorScheduleHandler> logger)
        {
            this.calendarRepository = calendarRepository;
            this.scheduleRepository = scheduleRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(CreateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var CalendarExist = await calendarRepository.GetAllAsync(x => x.Time.Date == request.CreateDoctorScheduleDtos.Time.Date && x.DoctorID == request.CreateDoctorScheduleDtos.DoctorID);
                if (CalendarExist.Count != 0)
                {
                    return ApiResponse.BadRequest("Calendar is exit");
                }
                var Calendar = await calendarRepository.CreateEntityAsync(new DoctorCalendar
                {
                    DoctorID = request.CreateDoctorScheduleDtos.DoctorID,
                    Time = request.CreateDoctorScheduleDtos.Time
                });
                if (Calendar == null)
                {
                    throw new Exception("Create Error");
                }
                var Schedules = mapper.Map<List<DoctorSchedule>>(request.CreateDoctorScheduleDtos.Slots);
                foreach (var Schedule in Schedules)
                {
                    Schedule.CalendarID = Calendar.CalenderID;
                }
                await scheduleRepository.CreateRangeAsync(Schedules);
                return ApiResponse.Created("Create Schedule Success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
