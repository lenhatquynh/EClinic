using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BlogService.Commands;
using Project.BlogService.Data;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;

namespace Project.BlogService.Handlers.HashTagHandlers;

public class UpdateHashtagHandler : IRequestHandler<UpdateHashtagCommands, ObjectResult>
{
    private readonly IMongoDBRepository<Hashtag> repository;
    private readonly ILogger<GetAllHashTagHandler> logger;

    public UpdateHashtagHandler(IMongoDBRepository<Hashtag> repository, ILogger<GetAllHashTagHandler> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task<ObjectResult> Handle(UpdateHashtagCommands request, CancellationToken cancellationToken)
    {
        try
        {
            var hashtag = await repository.GetAsync(request.UpdateHashtagDtos.Id);
            if (hashtag == null)
            {
                return ApiResponse.NotFound("Hashtag not found.");
            }
            hashtag.HashtagName = request.UpdateHashtagDtos.HashtagName;
            await repository.UpdateAsync(hashtag);
            return ApiResponse.OK("Update Hashtag success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }
    }
}
