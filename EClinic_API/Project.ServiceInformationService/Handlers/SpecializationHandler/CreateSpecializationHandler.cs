using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Repository.SpecializationRepository;
using System.Numerics;

namespace Project.ServiceInformationService.Handlers.SpecializationHandler;

public class CreateSpecializationHandler : IRequestHandler<CreateSpecializationCommand, ObjectResult>
{
    private readonly ILogger<CreateSpecializationHandler> logger;
    private readonly ISpecializationRepository repository;
    private readonly IMapper mapper;

    public CreateSpecializationHandler(ILogger<CreateSpecializationHandler> logger, ISpecializationRepository repository, IMapper mapper)
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<ObjectResult> Handle(CreateSpecializationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.createSpecializationDTO.SpecializationName == null 
                || request.createSpecializationDTO.SpecializationName.Trim().Count() == 0)
            {
                return ApiResponse.BadRequest("Create Specialization Error: Specialization Name must be not null!");
            }

            var existSpecialization = await repository.AnyAsync(x => String.Equals(x.SpecializationName, request.createSpecializationDTO.SpecializationName));
            if (existSpecialization)
            {
                return ApiResponse.BadRequest("Specialization name is exist.");
            }

            Specialization specialization = mapper.Map<Specialization>(request.createSpecializationDTO);

            await repository.CreateAsync(specialization);

            return ApiResponse.OK("Create Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
