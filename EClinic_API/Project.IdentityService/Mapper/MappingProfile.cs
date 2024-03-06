using AutoMapper;
using Project.IdentityService.Data;
using Project.IdentityService.Dtos;

namespace Project.IdentityService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUsersDtos>()
                .ForMember(
                    des => des.RoleName,
                    opt => opt.MapFrom(src => src.Role.RoleName)
                )
                .ReverseMap();
            CreateMap<Role, RoleDtos>()
                .ReverseMap();
        }
    }
}
