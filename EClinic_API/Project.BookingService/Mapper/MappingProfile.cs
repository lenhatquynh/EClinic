using AutoMapper;
using Project.BookingService.Data;
using Project.BookingService.Dtos.BookingDoctorDtos;
using Project.BookingService.Dtos.BookingPackageDTOs;
using Project.BookingService.Dtos.DoctorScheduleDtos;

namespace Project.BookingService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region BookingPackage
            CreateMap<BookingPackage, BookingPackageDTO>()
                .ForMember(
                    des => des.BookingStatus,
                    opt => opt.MapFrom(src => src.BookingStatus.ToString())
                )
                .ReverseMap();
            CreateMap<BookingPackage, BookingPackageDetail>()
                .ForMember(
                    des => des.BookingStatus,
                    opt => opt.MapFrom(src => src.BookingStatus.ToString())
                )
                .ReverseMap();
            CreateMap<BookingPackage, CreateBookingPackageDTO>()
                .ReverseMap();
            #endregion
            #region BookingDoctor
            CreateMap<BookingDoctor, BookingDoctorDTO>()
                .ForMember(
                    des => des.BookingStatus,
                    opt => opt.MapFrom(src => src.BookingStatus.ToString())
                )
                .ReverseMap();
            CreateMap<BookingDoctor, BookingDoctorDetail>()
                .ForMember(
                    des => des.BookingStatus,
                    opt => opt.MapFrom(src => src.BookingStatus.ToString())
                )
                .ReverseMap();
            CreateMap<BookingDoctor, CreateBookingDoctorDto>()
                .ReverseMap();
            #endregion
            #region Doctor Schedule
            CreateMap<CreateSlotDtos, DoctorSchedule>()
                .ForMember(
                    des => des.StartTime,
                    opt => opt.MapFrom(src => DateTime.Parse(src.StartTime))
                )
                .ForMember(
                    des => des.EndTime,
                    opt => opt.MapFrom(src => DateTime.Parse(src.EndTime))
                )
                .ReverseMap();
            CreateMap<UpdateSlotDtos, DoctorSchedule>()
                .ForMember(
                   des => des.ScheduleID,
                   opt => opt.MapFrom(src => src.SlotID)
               )
               .ForMember(
                   des => des.StartTime,
                   opt => opt.MapFrom(src => DateTime.Parse(src.StartTime))
               )
               .ForMember(
                   des => des.EndTime,
                   opt => opt.MapFrom(src => DateTime.Parse(src.EndTime))
               )
               .ReverseMap();
            CreateMap<DoctorSchedule, SlotDtos>()
                .ForMember(
                    des => des.SlotID,
                    opt => opt.MapFrom(src => src.ScheduleID)
                )
                .ForMember(
                    des => des.StartTime,
                    opt => opt.MapFrom(src => src.StartTime.ToString("HH:mm"))
                )
                .ForMember(
                    des => des.EndTime,
                    opt => opt.MapFrom(src => src.EndTime.ToString("HH:mm"))
                )
                .ForMember(
                    des => des.IsBooking,
                    opt => opt.MapFrom(src => src.BookingDoctor == null ? false : true)
                )
                .ReverseMap();
            #endregion

        }
    }
}
