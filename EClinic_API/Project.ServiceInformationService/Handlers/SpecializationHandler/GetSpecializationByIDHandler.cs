using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.Data.Repository.MongoDB;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Dtos.SpecializationDTOs;
using Project.ServiceInformationService.Queries;
using Project.ServiceInformationService.Repository.SpecializationRepository;

namespace Project.BlogService.Handlers.SpecializationHandler
{
    public class GetSpecializationByIDHandler : IRequestHandler<GetSpecializationByIDQuery, ObjectResult>
    {
        private readonly ISpecializationRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<GetSpecializationByIDHandler> logger;

        public GetSpecializationByIDHandler(ISpecializationRepository repository, IMapper mapper, ILogger<GetSpecializationByIDHandler> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ObjectResult> Handle(GetSpecializationByIDQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Specialization specialization = await repository.GetAsync(request.specializationID);
                if (specialization == null)
                {
                    return ApiResponse.NotFound("Specialization Not Found.");
                }
              
                SpecializationDTO specializationDTO = mapper.Map<SpecializationDTO>(specialization);

                return ApiResponse.OK(specializationDTO);
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return ApiResponse.InternalServerError();
            }
        }
    }
}
