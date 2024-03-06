using Project.ServiceInformationService.Data;
using Project.ServiceInformationService.Dtos.ServiceDTOs;
using Project.ServiceInformationService.Dtos.ServicePackageDTOs;
using Project.ServiceInformationService.Dtos.SpecializationDTOs;

namespace Project.ServiceInformationService.Mapper
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            #region Specialization
            CreateMap<Specialization, SpecializationDTO>()
                .ReverseMap(); 
            
            CreateMap<Specialization, CreateSpecializationDTO>()
                .ReverseMap();

            CreateMap<Specialization, UpdateSpecializationDTO>()
                .ReverseMap();
            #endregion
            #region Service
            CreateMap<Data.Service, ServiceDTO>()
                .ReverseMap();

            CreateMap<Data.Service, CreateServiceDTO>()
                .ReverseMap();

            CreateMap<Data.Service, UpdateServiceDTO>()
                .ReverseMap();
            #endregion
            #region Service Package
            CreateMap<ServicePackage, ServicePackageDTO>()
                .ReverseMap();

            CreateMap<ServicePackage, CreateServicePackageDTO>()
                .ReverseMap();

            CreateMap<ServicePackage, UpdateServicePackageDTO>()
                .ReverseMap();
            #endregion

        }
    }
}
