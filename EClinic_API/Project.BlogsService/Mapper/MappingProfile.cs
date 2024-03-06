using AutoMapper;
using Project.BlogService.Data;
using Project.BlogService.Dtos.BlogDtos;
using Project.BlogService.Dtos.BlogsDtos;
using Project.BlogService.Dtos.HashtagDtos;

namespace Project.BlogService.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Blog, BlogDtos>()
                .ReverseMap();

        CreateMap<Blog, CreateBlogDtos>()
           .ReverseMap();

        CreateMap<Hashtag, HashtagsDtos>()
            .ForMember(
              des => des.Id,
              opt => opt.MapFrom(src => src.Id)
          )
           .ReverseMap();
    }
}
