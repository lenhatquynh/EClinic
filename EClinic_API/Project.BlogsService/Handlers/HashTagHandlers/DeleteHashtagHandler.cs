using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BlogService.Commands;
using Project.BlogService.Data;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;

namespace Project.BlogService.Handlers.HashTagHandlers;

public class DeleteHashtagHandler : IRequestHandler<DeleteHashtagCommands, ObjectResult>
{
    private readonly IMongoDBRepository<Hashtag> repository;
    private readonly ILogger<DeleteHashtagHandler> logger;

    public DeleteHashtagHandler(IMongoDBRepository<Hashtag> repository, ILogger<DeleteHashtagHandler> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task<ObjectResult> Handle(DeleteHashtagCommands request, CancellationToken cancellationToken)
    {
        try
        {
            var hashtags = await repository.GetAsync(request.HashtagID);
            if (hashtags == null)
            {
                return ApiResponse.NotFound("Hashtag not found.");
            }
          
            await repository.RemoveAsync(request.HashtagID);
            return ApiResponse.OK("Delete success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }
    }
}
