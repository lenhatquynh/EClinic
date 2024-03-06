using AutoMapper;
using Project.PaymentService.Data;
using Project.PaymentService.Model;

namespace Project.PaymentService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Payment, PaymentDtos>()
                .ForPath(
                    des => des.IsRefund,
                    opt => opt.MapFrom(src => src.Refund == null ? false : true)
                )
                .ForPath(
                    des => des.PaymentService,
                    opt => opt.MapFrom(src => src.PaymentService == 0 ? "Momo" : "VNPay")
                )
                .ReverseMap();
            CreateMap<Refund, RefundDtos>()
                .ForPath(
                    des => des.PaymentService,
                    opt => opt.MapFrom(src => src.Payment.PaymentService == 0 ? "Momo" : "VNPay")
                )
                .ReverseMap();
            CreateMap<Payment, PaymentDetailDtos>()
               .ForPath(
                   des => des.IsRefund,
                   opt => opt.MapFrom(src => src.Refund == null ? false : true)
               )
               .ForPath(
                   des => des.PaymentService,
                   opt => opt.MapFrom(src => src.PaymentService == 0 ? "Momo" : "VNPay")
               )
               .ReverseMap();
            CreateMap<Refund, RefundDetailDtos>()
                .ForPath(
                    des => des.PaymentService,
                    opt => opt.MapFrom(src => src.Payment.PaymentService == 0 ? "Momo" : "VNPay")
                )
                .ReverseMap();
        }
    }
}
