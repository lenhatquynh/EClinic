using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BlogService.Commands;
using Project.Common.Enum;
using Project.Common.Response;
using Project.Core.AWS;
using Project.Core.Logger;

namespace Project.BlogService.Handlers.BlogHandlers
{
    public class UploadImageHandler : IRequestHandler<UploadImageCommands, ObjectResult>
    {
        private readonly ILogger<UploadImageHandler> logger;
        private readonly IAmazonS3Bucket bucket;

        public UploadImageHandler(ILogger<UploadImageHandler> logger, IAmazonS3Bucket bucket)
        {
            this.logger = logger;
            this.bucket = bucket;
        }

        public async Task<ObjectResult> Handle(UploadImageCommands request, CancellationToken cancellationToken)
        {
            try
            {
                string image = "";
                if (request.image is not null)
                {
                    image = await bucket.UploadFileAsync(request.image, FileType.Image);
                }
                return ApiResponse.OK(image);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
