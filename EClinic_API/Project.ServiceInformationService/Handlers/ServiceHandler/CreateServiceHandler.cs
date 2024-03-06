using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Repository.ServiceRepository;
using Project.ServiceInformationService.Repository.SpecializationRepository;

namespace Project.ServiceInformationService.Handlers.ServiceHandler;

public class CreateServiceHandler : IRequestHandler<CreateServiceCommand, ObjectResult>
{
    private readonly ILogger<CreateServiceHandler> logger;
    private readonly IServiceRepository repository;
    private readonly ISpecializationRepository specializationRepository;
    private readonly IMapper mapper;

    public CreateServiceHandler(ILogger<CreateServiceHandler> logger, IServiceRepository repository, ISpecializationRepository specializationRepository, IMapper mapper)
    {
        this.logger = logger;
        this.repository = repository;
        this.specializationRepository = specializationRepository;
        this.mapper = mapper;
    }

    public async Task<ObjectResult> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.createServiceDTO.ServiceName == null
                || request.createServiceDTO.ServiceName.Trim().Count() == 0)
            {
                return ApiResponse.BadRequest("Create Service Error: Service Name must be not null!");
            }

            var existSpecialization = await specializationRepository.AnyAsync(request.createServiceDTO.SpecializationID);
            if (!existSpecialization)
            {
                return ApiResponse.BadRequest("Specialization ID is not exist.");
            }

            Data.Service service = mapper.Map<Data.Service>(request.createServiceDTO);
            service.CreatedAt = DateTime.Now;
            service.UpdatedAt = DateTime.Now;

            await repository.CreateAsync(service);

            return ApiResponse.OK("Create Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
