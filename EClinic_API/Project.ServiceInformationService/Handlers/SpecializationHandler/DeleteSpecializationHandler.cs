using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Common.Response;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Repository.SpecializationRepository;

namespace Project.ServiceInformationService.Handlers.SpecializationHandler;

public class DeleteSpecializationHandler : IRequestHandler<DeleteSpecializationCommand, ObjectResult>
{
    private readonly ILogger<DeleteSpecializationHandler> logger;
    private readonly ISpecializationRepository repository;
    private readonly IMapper mapper;

    public DeleteSpecializationHandler(ILogger<DeleteSpecializationHandler> logger, ISpecializationRepository repository, IMapper mapper)
    {
        this.logger = logger;
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<ObjectResult> Handle(DeleteSpecializationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var specialization = await repository.GetAsync(request.deleteSpecializationDTO.SpecializationID);

            if (specialization is null)
            {
                return ApiResponse.BadRequest("Specialization not found!");
            }

            await repository.DeleteAsync(specialization);

            return ApiResponse.OK("Delete Success.");
        }
        catch (Exception ex)
        {
            logger.WriteLogError(ex.Message);
            return ApiResponse.InternalServerError();
        }

    }
}
