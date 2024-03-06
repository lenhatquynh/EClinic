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
    public class UpdateDoctorScheduleHandler : IRequestHandler<UpdateDoctorScheduleCommand, ObjectResult>
    {
        private readonly IDoctorCalendarRepository calendarRepository;
        private readonly IDoctorScheduleRepository scheduleRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateDoctorScheduleHandler> logger;

        public UpdateDoctorScheduleHandler(IDoctorCalendarRepository calendarRepository, IDoctorScheduleRepository scheduleRepository, IMapper mapper, ILogger<UpdateDoctorScheduleHandler> logger)
        {
            this.calendarRepository = calendarRepository;
            this.scheduleRepository = scheduleRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(UpdateDoctorScheduleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var Calendar = await calendarRepository.GetDoctorCalendarAsync(x => x.CalenderID == request.UpdateDoctorScheduleDtos.CalenderID);
                var Schedules = Calendar.DoctorSchedules;
                var ScheduleDtos = mapper.Map<List<DoctorSchedule>>(request.UpdateDoctorScheduleDtos.Slots);
                var scheduleIDs = ScheduleDtos.Select(s => s.ScheduleID).ToList();
                List<DoctorSchedule> UpdateSchedules = new List<DoctorSchedule>();
                List<DoctorSchedule> CreateSchedules = new List<DoctorSchedule>();
                List<DoctorSchedule> DeleteSchedule = new List<DoctorSchedule>();
                foreach (var schedule in Schedules)
                {
                    if (scheduleIDs.Contains(schedule.ScheduleID))
                    {
                        var matchingSchedule = ScheduleDtos.FirstOrDefault(s => s.ScheduleID == schedule.ScheduleID);
                        if (matchingSchedule != null &&
                            (matchingSchedule.StartTime.ToString("HH:mm") != schedule.StartTime.ToString("HH:mm") ||
                             matchingSchedule.EndTime.ToString("HH:mm") != schedule.EndTime.ToString("HH:mm")))
                        {
                            matchingSchedule.CalendarID = Calendar.CalenderID;
                            UpdateSchedules.Add(matchingSchedule);
                        }
                    }
                    else
                    {
                        DeleteSchedule.Add(schedule);
                    }
                }
                CreateSchedules = ScheduleDtos.Where(x => x.ScheduleID == Guid.Empty).ToList();
                foreach(var schedule in CreateSchedules)
                {
                    schedule.CalendarID = Calendar.CalenderID;
                }
                await scheduleRepository.UpdateRangeAsync(UpdateSchedules);
                await scheduleRepository.DeleteRangeAsync(DeleteSchedule);
                await scheduleRepository.CreateRangeAsync(CreateSchedules);
                return ApiResponse.OK("Update Success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
