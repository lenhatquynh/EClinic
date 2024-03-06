using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BlogService.Dtos.BlogDtos;
using Project.BlogService.Dtos.BlogsDtos;
using Project.BlogService.Dtos.HashtagDtos;

namespace Project.BlogService.Commands
{
    #region BlogCommands
    public record CreateBlogCommands(CreateBlogDtos createBlogDtos, string UserId) : IRequest<ObjectResult>;
    public record UpdateBlogCommands(UpdateBlogDtos updateBlogDtos) : IRequest<ObjectResult>;
    public record DeleteBlogCommands(Guid BlogID) : IRequest<ObjectResult>;
    public record UploadImageCommands(IFormFile image) : IRequest<ObjectResult>;
    #endregion
    #region HashtagCommands
    public record CreateHashtagCommands(string HashtagName) : IRequest<ObjectResult>;
    public record UpdateHashtagCommands(UpdateHashtagDtos UpdateHashtagDtos) : IRequest<ObjectResult>;
    public record DeleteHashtagCommands(Guid HashtagID) : IRequest<ObjectResult>;
    #endregion
}
