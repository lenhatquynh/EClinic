using AutoMapper;
using Project.ForumService.Data;
using Project.ForumService.Dtos.AnswersDtos;
using Project.ForumService.Dtos.CommentsDtos;
using Project.ForumService.Dtos.HashtagDtos;
using Project.ForumService.Dtos.PostsDtos;
using System.Linq;
using System.Linq.Expressions;
using System.Net;

namespace Project.ForumService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDtos>()
                .ForMember(
                    des => des.Likes,
                    opt => opt.MapFrom(src => src.LikeUserIds.Count())
                )
                .ReverseMap();
            CreateMap<Post, CreatePostDtos>()
               .ReverseMap();

            CreateMap<Answer, AnswerDtos>()
                .ForMember(
                    des => des.Likes,
                    opt => opt.MapFrom(src => src.LikeUserIds.Count())
                )
                .ReverseMap();
            CreateMap<Answer, CreateAnswerDtos>()
               .ReverseMap();

            CreateMap<Comment, CommentDtos>()
              .ForMember(
                  des => des.Likes,
                  opt => opt.MapFrom(src => src.LikeUserIds.Count())
              )
              .ForMember(
                  des => des.ReplyCommentDtos,
                  opt => opt.MapFrom(src =>src.ReplyComments)
              )
              .ReverseMap();
            CreateMap<ReplyComment, ReplyCommentDtos>()
                .ForMember(
                  des => des.Likes,
                  opt => opt.MapFrom(src => src.LikeUserIds.Count())
              )
               .ReverseMap();
            CreateMap<Comment, CreateCommentDtos>()
               .ReverseMap();
            CreateMap<Comment, CreateReplyCommentDtos>()
               .ReverseMap();
            CreateMap<ReplyComment, CreateCommentDtos>()
               .ReverseMap();
            CreateMap<ReplyComment, CreateReplyCommentDtos>()
               .ReverseMap();
            CreateMap<Hashtag, HashtagsDtos>()
                .ForMember(
                  des => des.HashtagID,
                  opt => opt.MapFrom(src => src.Id)
              )
               .ReverseMap();
        }
    }
}
