//using MassTransit;
//using Project.Common.Constants;
//using Project.Core.Logger;
//using Project.Core.RabbitMQ;
//using Project.Data.Repository.MongoDB;
//using Project.BlogService.Data;
//using Project.BlogService.Events;

//namespace Project.BlogService.Consumer
//{
//    public class DeleteProfileConsumer : IConsumer<DeleteProfileEvents>
//    {
//        private readonly IMongoDBRepository<Blog> blogRepository;
//        private readonly ILogger<DeleteProfileConsumer> logger;
//        private readonly IBus     ;

//        public DeleteProfileConsumer(IMongoDBRepository<Blog> postRepository, IMongoDBRepository<Answer> answerRepository, IMongoDBRepository<Comment> commentRepository, ILogger<DeleteProfileConsumer> logger, IBus bus)
//        {
//            this.postRepository = postRepository;
//            this.answerRepository = answerRepository;
//            this.commentRepository = commentRepository;
//            this.logger = logger;
//            this.bus = bus;
//        }

//        public async Task Consume(ConsumeContext<DeleteProfileEvents> context)
//        {
//            try
//            {
//                var UserID = context.Message.UserID;
//                var posts = await postRepository.GetAllAsync(x => x.Author.UserID == UserID);
//                var answer = await answerRepository.GetAllAsync(x => x.Author.UserID == UserID);
//                var comments = await commentRepository.GetAllAsync();
//                await postRepository.RemoveManyAsync(posts.Select(x => x.Id).ToList());
//                await answerRepository.RemoveManyAsync(answer.Select(x => x.Id).ToList());
//                foreach (var comment in comments)
//                {
//                    if (comment.Author.UserID == UserID)
//                    {
//                        await commentRepository.RemoveAsync(comment.Id);
//                        continue;
//                    }
//                    foreach (var replyComment in comment.ReplyComments)
//                    {
//                        if (replyComment.Author.UserID == UserID)
//                        {
//                            comment.ReplyComments.Remove(replyComment);
//                            await commentRepository.UpdateAsync(comment);
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                logger.WriteLogError(ex.Message);
//                await bus.SendMessageWithExchangeName<DeleteProfileResultEvents>(new DeleteProfileResultEvents
//                {
//                    IsSuccess = false,
//                    UserID = context.Message.UserID
//                }, ExchangeConstants.ProfileService); ;
//            }
//        }
//    }
//}
