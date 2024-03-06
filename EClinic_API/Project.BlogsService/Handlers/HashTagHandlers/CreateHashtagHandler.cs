using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.BlogService.Commands;
using Project.BlogService.Data;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;

namespace Project.BlogService.Handlers.HashTagHandlers;

public class CreateHashtagHandler : IRequestHandler<CreateHashtagCommands, ObjectResult>
{
    private readonly IMongoDBRepository<Hashtag> repository;
    private readonly ILogger<CreateHashtagHandler> logger;

    public CreateHashtagHandler(IMongoDBRepository<Hashtag> repository, ILogger<CreateHashtagHandler> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task<ObjectResult> Handle(CreateHashtagCommands request, CancellationToken cancellationToken)
    {
        try
        {
            var oldHashtag = await repository.GetAsync(x => String.Equals(x.HashtagName, request.HashtagName));
            if (oldHashtag != null)
            {
                return ApiResponse.BadRequest("Hashtag name is exist.");
            }
            Hashtag hashtag = new Hashtag { HashtagName = request.HashtagName, Count = 0 };
            await repository.CreateAsync(hashtag);
            return ApiResponse.OK("Create Success.");

        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }
    }
}
