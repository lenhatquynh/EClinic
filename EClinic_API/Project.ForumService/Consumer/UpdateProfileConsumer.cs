using MassTransit;
using Project.Common.Constants;
using Project.Core.Logger;
using Project.Core.RabbitMQ;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Data;
using Project.ForumService.Events;
using Project.ProfileServices.Events;

namespace Project.ForumService.Consumer
{
    public class UpdateProfileConsumer : IConsumer<UpdateProfileEvents>
    {
        private readonly IMongoDBRepository<Post> postRepository;
        private readonly IMongoDBRepository<Answer> answerRepository;
        private readonly IMongoDBRepository<Comment> commentRepository;
        private readonly ILogger<DeleteProfileConsumer> logger;
        private readonly IBus bus;

        public UpdateProfileConsumer(IMongoDBRepository<Post> postRepository, IMongoDBRepository<Answer> answerRepository, IMongoDBRepository<Comment> commentRepository, ILogger<DeleteProfileConsumer> logger, IBus bus)
        {
            this.postRepository = postRepository;
            this.answerRepository = answerRepository;
            this.commentRepository = commentRepository;
            this.logger = logger;
            this.bus = bus;
        }

        public async Task Consume(ConsumeContext<UpdateProfileEvents> context)
        {
            try
            {
                var UserID = context.Message.UserID;
                var FirstName = context.Message.FirstName;
                var LastName = context.Message.LastName;
                var Avatar = context.Message.Avatar;
                var posts = await postRepository.GetAllAsync(x => x.Author.UserID == UserID);
                var answers = await answerRepository.GetAllAsync(x => x.Author.UserID == UserID);
                var comments = await commentRepository.GetAllAsync();
                foreach (var post in posts)
                {
                    post.Author.FirstName = FirstName;
                    post.Author.Avatar = Avatar;
                    post.Author.LastName = LastName;
                    await postRepository.UpdateAsync(post);
                }
                foreach (var answer in answers)
                {
                    answer.Author.FirstName = FirstName;
                    answer.Author.Avatar = Avatar;
                    answer.Author.LastName = LastName;
                    await answerRepository.UpdateAsync(answer);
                }
                foreach (var comment in comments)
                {
                    if (comment.Author.UserID == UserID)
                    {
                        comment.Author.FirstName = FirstName;
                        comment.Author.Avatar = Avatar;
                        comment.Author.LastName = LastName;
                        await commentRepository.UpdateAsync(comment);
                    }
                    foreach (var replyComment in comment.ReplyComments)
                    {
                        if (replyComment.Author.UserID == UserID)
                        {
                            comment.ReplyComments.Remove(replyComment);
                            replyComment.Author.FirstName = FirstName;
                            replyComment.Author.Avatar = Avatar;
                            replyComment.Author.LastName = LastName;
                            comment.ReplyComments.Add(replyComment);
                            await commentRepository.UpdateAsync(comment);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                await bus.SendMessageWithExchangeName<UpdateProfileResultEvents>(new UpdateProfileResultEvents
                {
                    IsSuccess = false,
                    Avatar = context.Message.Avatar,
                    FirstName = context.Message.FirstName,
                    LastName = context.Message.LastName,
                    UserID = context.Message.UserID
                }, ExchangeConstants.ProfileService); ;
            }
        }
    }
}
