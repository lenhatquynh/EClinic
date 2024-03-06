using AutoMapper;
using Project.CommunicateService.Data;
using Project.CommunicateService.Dtos.ChatMessageDtos;
using Project.CommunicateService.Dtos.RoomDtos;
using Project.CommunicateService.Dtos.RoomTypeDtos;

namespace Project.CommunicateService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomDto>()
                .ForPath(
                    des => des.ChatMessage.ChatMessageID,
                    opt => opt.MapFrom(src => src.ChatMessages.FirstOrDefault().ChatMessageID)
                )
                .ForPath(
                    des => des.ChatMessage.UserID,
                    opt => opt.MapFrom(src => src.ChatMessages.FirstOrDefault().UserID)
                )
                .ForPath(
                    des => des.ChatMessage.Content,
                    opt => opt.MapFrom(src => src.ChatMessages.FirstOrDefault().Content)
                )
                .ForPath(
                    des => des.ChatMessage.IsImage,
                    opt => opt.MapFrom(src => src.ChatMessages.FirstOrDefault().Type == MessageType.Image ? true : false)
                )
                .ForPath(
                    des => des.ChatMessage.CreatedAt,
                    opt => opt.MapFrom(src => src.ChatMessages.FirstOrDefault().CreatedAt)
                )
                .ForPath(
                    des => des.RoomType.RoomTypeID,
                    opt => opt.MapFrom(src => src.RoomType.RoomTypeID)
                )
                .ForPath(
                    des => des.RoomType.RoomTypeName,
                    opt => opt.MapFrom(src => src.RoomType.RoomTypeName)
                )
                .ReverseMap();
            CreateMap<RoomType, RoomTypeDto>()
               .ReverseMap();
            CreateMap<ChatMessage, ChatMessageDto>()
                .ForPath(
                    des => des.IsImage,
                    opt => opt.MapFrom(src => src.Type == MessageType.Image ? true : false)
                )
               .ReverseMap();

        }
    }
}
