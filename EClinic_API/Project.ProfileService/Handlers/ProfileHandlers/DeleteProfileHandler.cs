using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;
using Project.Core.RabbitMQ;
using Project.ProfileService.Commands;
using Project.ProfileService.Events;
using Project.ProfileService.Repository.ProfileRepository;

namespace Project.ProfileService.Handlers.ProfileHandlers
{
    public class DeleteProfileHandler : IRequestHandler<DeleteProfileCommands, ObjectResult>
    {
        private readonly IProfileRepository profileRepository;
        private readonly IAmazonS3Bucket s3Bucket;
        private readonly ILogger<DeleteProfileHandler> logger;
        private readonly IBus bus;

        public DeleteProfileHandler(IProfileRepository profileRepository, IAmazonS3Bucket s3Bucket, ILogger<DeleteProfileHandler> logger, IBus bus)
        {
            this.profileRepository = profileRepository;
            this.s3Bucket = s3Bucket;
            this.logger = logger;
            this.bus = bus;
        }

        public async Task<ObjectResult> Handle(DeleteProfileCommands request, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await profileRepository.GetAsync(request.ProfileID);
                if (profile == null)
                {
                    return ApiResponse.NotFound("Profile Not Found.");
                }
                var result = await profileRepository.DeleteAsync(profile);
                if (!result)
                {
                    throw new Exception("Delete Profile Error.");
                }
                await bus.SendMessage<DeleteProfileEvents>(new DeleteProfileEvents { UserID = profile.UserID });
                if (!String.IsNullOrEmpty(profile.Avatar))
                {
                    await s3Bucket.DeleteFileAsync(profile.Avatar);
                }
                return ApiResponse.OK("Delete Profile Success");
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
