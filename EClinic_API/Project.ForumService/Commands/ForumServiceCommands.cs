using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.ForumService.Dtos.AnswersDtos;
using Project.ForumService.Dtos.CommentsDtos;
using Project.ForumService.Dtos.HashtagDtos;
using Project.ForumService.Dtos.PostsDtos;

namespace Project.ForumService.Commands
{
    #region PostCommands
    public record CreatePostCommands(CreatePostDtos createPostDtos, string UserID) : IRequest<ObjectResult>;
    public record UpdatePostCommands(UpdatePostDtos updatePostDtos) : IRequest<ObjectResult>;
    public record DeletePostCommands(Guid PostID) : IRequest<ObjectResult>;
    public record LikePostCommands(Guid PostID, string UserID) : IRequest<ObjectResult>;
    public record AcceptPostCommands(Guid PostID) : IRequest<ObjectResult>;
    #endregion
    #region AnswerCommands
    public record CreateAnswerCommands(CreateAnswerDtos CreateAnswerDtos, string UserID) : IRequest<ObjectResult>;
    public record UpdateAnswerCommands(UpdateAnswerDtos UpdateAnswerDtos) : IRequest<ObjectResult>;
    public record DeleteAnswerCommands(Guid AnswerID) : IRequest<ObjectResult>;
    public record LikeAnswerCommands(Guid AnswerID, string UserID) : IRequest<ObjectResult>;
    #endregion
    #region CommentCommands
    public record CreateCommentCommands(CreateCommentDtos CreateCommentDtos, string UserID) : IRequest<ObjectResult>;
    public record CreateReplyCommentCommands(CreateReplyCommentDtos CreateReplyCommentDtos, string UserID) : IRequest<ObjectResult>;
    public record UpdateCommentCommands(UpdateCommentDtos UpdateCommentDtos) : IRequest<ObjectResult>;
    public record UpdateReplyCommentCommands(UpdateReplyCommentDtos UpdateReplyCommentDtos) : IRequest<ObjectResult>;
    public record DeleteCommentCommands(Guid CommentID) : IRequest<ObjectResult>;
    public record DeleteReplyCommentCommands(Guid ParentCommentID, Guid CommentID) : IRequest<ObjectResult>;
    public record LikeCommentCommands(Guid CommentID, string UserID) : IRequest<ObjectResult>;
    public record LikeReplyCommentCommands(Guid ParentCommentID, Guid CommentID, string UserID) : IRequest<ObjectResult>;
    #endregion
    #region HashtagCommands
    public record CreateHashtagCommands(string HashtagName) : IRequest<ObjectResult>;
    public record UpdateHashtagCommands(UpdateHashtagDtos UpdateHashtagDtos) : IRequest<ObjectResult>;
    public record DeleteHashtagCommands(Guid HashtagID) : IRequest<ObjectResult>;
    #endregion
}
