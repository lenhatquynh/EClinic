using MassTransit;
using Project.BlogService.Data;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ProfileServices.Events;

namespace Project.BlogService.Consumer
{
    public class UpdateProfileConsumer : IConsumer<UpdateProfileEvents>
    {
        private readonly IMongoDBRepository<Blog> postRepository;
        private readonly ILogger<UpdateProfileConsumer> logger;

        public UpdateProfileConsumer(IMongoDBRepository<Blog> postRepository, ILogger<UpdateProfileConsumer> logger)
        {
            this.postRepository = postRepository;
            this.logger = logger;
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
                foreach (var post in posts)
                {
                    post.Author.FirstName = FirstName;
                    post.Author.Avatar = Avatar;
                    post.Author.LastName = LastName;
                    await postRepository.UpdateAsync(post);
                }
                
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
            }
        }
    }
}
