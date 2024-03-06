using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Common.Paging;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ProfileService.Dtos.UserProfile;
using Project.ProfileService.Queries;
using Project.ProfileService.Repository.ProfileRepository;
using Project.ProfileService.Repository.RelationshipRepository;

namespace Project.ProfileService.Handlers.UserProfileHandlers
{
    public class SearchFamilyProfileHandler : IRequestHandler<SearchFamilyProfileQuery, ObjectResult>
    {
        private readonly IProfileRepository profileRepository;
        private readonly IRelationshipRepository relationshipRepository;
        private readonly ILogger<SearchFamilyProfileHandler> logger;
        private readonly IMapper mapper;

        public SearchFamilyProfileHandler(IProfileRepository profileRepository, IRelationshipRepository relationshipRepository, ILogger<SearchFamilyProfileHandler> logger, IMapper mapper)
        {
            this.profileRepository = profileRepository;
            this.relationshipRepository = relationshipRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<ObjectResult> Handle(SearchFamilyProfileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var UserID = Guid.Parse(request.UserID);
                var SearchText = request.SearchText == null ? "" : request.SearchText.ToLower();
                var profile = await profileRepository.GetProfilesAsync(UserID);
                profile = profile.Where(x => x.FirstName.ToLower().Contains(SearchText) || x.LastName.ToLower().Contains(SearchText)).ToList();

                PaginationResponseHeader header = new PaginationResponseHeader();
                header.TotalCount = profile.Count;
                profile = profile
                    .Skip((request.PaginationRequestHeader.PageNumber - 1) * request.PaginationRequestHeader.PageSize)
                    .Take(request.PaginationRequestHeader.PageSize).ToList();
                header.PageIndex = request.PaginationRequestHeader.PageNumber;
                header.PageSize = request.PaginationRequestHeader.PageSize;
                request.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(header));
                var profileDtos = mapper.Map<List<UserProfileDtos>>(profile);
                foreach (var pro in profileDtos)
                {
                    var relationship = await relationshipRepository.GetAsync(pro.RelationshipID);
                    pro.RelationshipName = relationship.RelationshipName;
                }
                return ApiResponse.OK<List<UserProfileDtos>>(profileDtos);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
