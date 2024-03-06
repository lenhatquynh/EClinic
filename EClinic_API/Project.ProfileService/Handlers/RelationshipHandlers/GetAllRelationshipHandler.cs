using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ProfileService.Dtos.Relationship;
using Project.ProfileService.Queries;
using Project.ProfileService.Repository.RelationshipRepository;

namespace Project.ProfileService.Handlers.RelationshipHandlers
{
    public class GetAllRelationshipHandler : IRequestHandler<GetAllRelationshipQuery, ObjectResult>
    {
        private readonly ILogger<GetAllRelationshipHandler> logger;
        private readonly IRelationshipRepository relationship;
        private readonly IMapper mapper;

        public GetAllRelationshipHandler(ILogger<GetAllRelationshipHandler> logger, IRelationshipRepository relationship, IMapper mapper)
        {
            this.logger = logger;
            this.relationship = relationship;
            this.mapper = mapper;
        }

        public async Task<ObjectResult> Handle(GetAllRelationshipQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var relationships = await relationship.GetAllAsync();
                if (relationships == null)
                {
                    return ApiResponse.NotFound("Relationship Not Found.");
                }

                var elationshipDtos = mapper.Map<List<RelationshipDtos>>(relationships);
                return ApiResponse.OK<List<RelationshipDtos>>(elationshipDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
