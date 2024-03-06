using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Repository.SpecializationRepository;

namespace Project.ServiceInformationService.Handlers.SpecializationHandler;

public class UpdateSpecializationHandler : IRequestHandler<UpdateSpecializationCommand, ObjectResult>
{
    private readonly ILogger<UpdateSpecializationHandler> logger;
    private readonly ISpecializationRepository repository;
    private readonly IMapper mapper;

    public UpdateSpecializationHandler(ILogger<UpdateSpecializationHandler> logger, ISpecializationRepository repository, IMapper mapper)
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<ObjectResult> Handle(UpdateSpecializationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.updateSpecializationDTO.SpecializationName == null
                || request.updateSpecializationDTO.SpecializationName.Trim().Count() == 0)
            {
                return ApiResponse.BadRequest("Update Specialization Error: Specialization Name must be not null!");
            }

            var specialization = await repository.GetAsync(request.updateSpecializationDTO.SpecializationID);

            if (specialization is null)
            {
                return ApiResponse.BadRequest("Specialization not found!");
            }

            var existSpecialization = await repository.AnyAsync(x => String.Equals(x.SpecializationName, request.updateSpecializationDTO.SpecializationName));
            if (existSpecialization)
            {
                return ApiResponse.BadRequest("Specialization name is exist.");
            }

            specialization = mapper.Map<Specialization>(request.updateSpecializationDTO);

            await repository.UpdateAsync(specialization);

            return ApiResponse.OK("Update Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
