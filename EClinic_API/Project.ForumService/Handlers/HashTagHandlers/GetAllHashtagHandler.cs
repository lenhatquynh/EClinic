using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Data.Repository.MongoDB;
using Project.ForumService.Data;
using Project.ForumService.Dtos.HashtagDtos;
using Project.ForumService.Queries;

namespace Project.ForumService.Handlers.HashTagHandlers
{
    public class GetAllHashTagHandler : IRequestHandler<GetAllHashtagQuery, ObjectResult>
    {
        private readonly IMongoDBRepository<Hashtag> repository;
        private readonly ILogger<GetAllHashTagHandler> logger;
        private readonly IMapper mapper;

        public GetAllHashTagHandler(IMongoDBRepository<Hashtag> repository, ILogger<GetAllHashTagHandler> logger, IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ObjectResult> Handle(GetAllHashtagQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var hashtags = await repository.GetAllAsync();
                List<HashtagsDtos> tagDtos = mapper.Map<List<HashtagsDtos>>(hashtags);
                return ApiResponse.OK<List<HashtagsDtos>>(tagDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
