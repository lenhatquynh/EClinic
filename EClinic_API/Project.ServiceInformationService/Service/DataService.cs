using Grpc.Core;
using MediatR;
using Project.Core.Logger;
using Project.ServiceInformationService.Commands;
using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Protos;
using Project.ServiceInformationService.Repository.ServicePackageRepository;
using Project.ServiceInformationService.Repository.SpecializationRepository;

namespace Project.ServiceInformationService.Service
{
    public class DataService : Protos.ServiceInformationService.ServiceInformationServiceBase
    {
        private readonly ISpecializationRepository repository;
        private readonly IServicePackageRepository servicePackageRepository;
        private readonly ILogger<DataService> logger;
        private IMediator mediator;

        public DataService(ISpecializationRepository repository, IServicePackageRepository servicePackageRepository, ILogger<DataService> logger, IMediator mediator)
        {
            this.repository = repository;
            this.servicePackageRepository = servicePackageRepository;
            this.logger = logger;
            this.mediator = mediator;
        }

        public override async Task<IncreaseOrderResponse> IncreaseOrder(IncreaseOrderRequest request, ServerCallContext context)
        {
            try
            {
                IncreaseOrderResponse response = new IncreaseOrderResponse();
                var increase = await mediator.Send(new IncreaseOrderCommand(Guid.Parse(request.ServicePackageID)));
                return response;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public override async Task<GetSpecializations> CheckSpecialization(GetSpecializationRequest request, ServerCallContext context)
        {
            try
            {
                var Specialization = await repository.GetAsync(Guid.Parse(request.SpecializationID));
                if (Specialization == null)
                {
                    return null;
                }
                return new GetSpecializations { SpecializationName = Specialization.SpecializationName, SpecializationID = Specialization.SpecializationID.ToString() };
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public override async Task<GetAllServicePackageResponse> GetAllServicePackage(GetAllServicePackageRequest request, ServerCallContext context)
        {
            try
            {
                var ServicePackageIDs = request.ServicePackageIDs.Select(Guid.Parse).ToList();
                var Specializations = await servicePackageRepository.GetAllAsync(x => ServicePackageIDs.Contains(x.ServicePackageID));
                if (Specializations == null)
                {
                    return null;
                }
                var result = new GetAllServicePackageResponse();
                List< GetServicePackageResponse> GetServicePackageResponse = new List<GetServicePackageResponse>();
                for (var i = 0; i < ServicePackageIDs.Count; i++)
                {
                    var sp = Specializations.SingleOrDefault(x => x.ServicePackageID == ServicePackageIDs[i]);
                    GetServicePackageResponse.Add(new GetServicePackageResponse
                    {
                        ServicePackageID = sp.ServicePackageID.ToString(),
                        ServicePackageName = sp.ServicePackageName,
                        Image = sp.Image

                    });

                }
                result.ServicePackage.AddRange(GetServicePackageResponse);
                return result;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public override async Task<GetAllSpecializationResponse> GetAllSpecialization(GetAllSpecializationRequest request, ServerCallContext context)
        {
            try
            {
                var SpecializationIds = request.SpecializationIDs.Select(Guid.Parse).ToList();
                var Specializations = await repository.GetAllAsync(x => SpecializationIds.Contains(x.SpecializationID));
                if (Specializations == null)
                {
                    return null;
                }
                var result = new GetAllSpecializationResponse();
                List<GetSpecializations> getSpecializations = new List<GetSpecializations>();
                for (var i = 0; i < SpecializationIds.Count; i++)
                {
                    var sp = Specializations.SingleOrDefault(x => x.SpecializationID == SpecializationIds[i]);
                    getSpecializations.Add(new GetSpecializations
                    {
                        SpecializationID = sp.SpecializationID.ToString(),
                        SpecializationName = sp.SpecializationName
                    });

                }
                result.Specialization.AddRange(getSpecializations);
                return result;
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }

        public override async Task<GetSpecializationResponse> GetSpecialization(GetSpecializationRequest request, ServerCallContext context)
        {
            try
            {
                var Specialization = await repository.GetAsync(Guid.Parse(request.SpecializationID));
                if (Specialization == null)
                {
                    return null;
                }
                return new GetSpecializationResponse { SpecializationName = Specialization.SpecializationName };
            }
            catch (Exception ex)
            {
                logger.WriteLogError(ex.Message);
                return null;
            }
        }
    }
}
